using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Updoor : MonoBehaviour
{
    public GameObject[] SRUp; // ���� ������Ʈ�� SpriteRenderer�� ���� ����
    public Camera cma;
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawn.GetComponent<zombiemove>().clear)
        {
            //Ŭ���� �� ������ ��
            SRUp[0].SetActive(false);
            SRUp[1].SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (spawn.GetComponent<zombiemove>().clear)
        {
            // �ε����� ĳ���� ��ġ �̵�
            collision.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y + 3.5f);
            cma.transform.position = new Vector3(cma.transform.position.x, cma.transform.position.y + 10, cma.transform.position.z);
        }
    }
}
