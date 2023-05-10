using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    GameObject player;
    GameObject[] fire = new GameObject[3];  // 총알 발사 위치
    public GameObject bulliet; //총알
    public GameObject effect;
    public Item bulletnum;

    //int maxBulliet = 999999; //소유한 탄알
    int bullietNumber = 10; // 장전된 탄알 수
    public int magazine = 10; //탄창최대 갯수

    float bullietTime = 0;
    float t = 1.5f;//연사속도
    float t1; //이펙트 켜지는 시간
    bool b;

    AudioSource ad;

    public enum State
    {
        Ready, //발사 준비
        Empty, //탄알 없음
        ReLoading, //장전중
        stop
    }
    public State state { get; set; } //현재 총의 상태

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

    //총구 방향
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

    //총알 발사
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

    //장전 키 입력
    public bool Reload()
    {
        if (state == State.ReLoading || bulletnum.num <= 0 || bullietNumber >= magazine)
        {
            return false;
        }

        StartCoroutine(GunReload());

        return true;
    }

    //장전중
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
