using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    GameObject player;
    GameObject[] fire = new GameObject[3];  // �Ѿ� �߻� ��ġ
    public GameObject bulliet; //�Ѿ�
    public GameObject effect;
    public Item bulletnum;

    //int maxBulliet = 999999; //������ ź��
    int bullietNumber = 10; // ������ ź�� ��
    public int magazine = 10; //źâ�ִ� ����

    float bullietTime = 0;
    float t = 1.5f;//����ӵ�
    float t1; //����Ʈ ������ �ð�
    bool b;

    AudioSource ad;

    public enum State
    {
        Ready, //�߻� �غ�
        Empty, //ź�� ����
        ReLoading, //������
        stop
    }
    public State state { get; set; } //���� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
        fire[0] = transform.GetChild(0).gameObject;
        fire[1] = transform.GetChild(1).gameObject;
        fire[2] = transform.GetChild(2).gameObject;
        bulliet.GetComponent<ShotGunBulliet>().damage = 2f;
        ad = GetComponent<AudioSource>();
        t1 = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        bullietTime += Time.deltaTime;
        if (Manager.instance != null && !Manager.instance.gameOver)
            Angle();
        if (t1 > 0.2f)
        {
            effect.SetActive(false);
        }
        if (b)
        {
            t1 += Time.deltaTime;
        }
        else
        {
            t1 = 1f;
        }
    }

    //�ѱ� ����
    void Angle()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (Mathf.Abs(transform.rotation.z) > 0.7f)
        {
            transform.localScale = new Vector3(0.125f, -0.125f, 1);
            //player.transform.localScale = new Vector3(-2, 2, 1);
        }
        else
        {
            transform.localScale = new Vector3(0.125f, 0.125f, 1);
            //player.transform.localScale = new Vector3(2, 2, 1);
        }
    }

    //�Ѿ� �߻�
    public void Shot()
    {
        if (state == State.Ready && bullietNumber > 0 && bullietTime >= t)
        {
            ad.Play();
            b = true;
            t1 = 0;
            bullietTime = 0;
            Instantiate(bulliet, fire[0].transform.position, fire[0].transform.rotation);
            Instantiate(bulliet, fire[1].transform.position, fire[1].transform.rotation);
            Instantiate(bulliet, fire[2].transform.position, fire[2].transform.rotation);
            if (t1 < 0.2f)
            {
                effect.SetActive(true);
            }
            bullietNumber--;
            if (bullietNumber <= 0)
            {
                state = State.Empty;
                effect.SetActive(false);
            }
        }
    }

    //���� Ű �Է�
    public bool Reload()
    {
        if (state == State.ReLoading || bulletnum.num <= 0 || bullietNumber >= magazine)
        {
            return false;
        }

        StartCoroutine(GunReload());

        return true;
    }

    //������
    IEnumerator GunReload()
    {

        state = State.ReLoading;
        yield return new WaitForSeconds(2f);

        int num = magazine - bullietNumber;

        if (num > bulletnum.num)
        {
            num = bulletnum.num;
        }
        bullietNumber += num;
        bulletnum.num -= num;
        state = State.Ready;
    }

}
