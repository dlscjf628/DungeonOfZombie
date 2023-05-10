using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBulliet : MonoBehaviour
{
    float speed = 15f; //�Ѿ� �ӵ�
    int cnt;
    public float damage; // { get; set; }  //�Ѿ� ������

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f); // �Ѿ� �߻� �� 5�ʵ� ����
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }


    //�Ѿ��� ���񿡰� �°ų� ���� ���� ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GunDamage target = collision.GetComponent<GunDamage>();
        if (collision.tag == "Zombie")
        {
            target.OnDamage(damage);
            cnt++;
            if(cnt==3)
                Destroy(gameObject);
        }

    }

}

