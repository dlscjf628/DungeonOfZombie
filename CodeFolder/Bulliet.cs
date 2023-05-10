using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulliet : MonoBehaviour
{
    float speed = 10f; //�Ѿ� �ӵ�
    public float damage = 5; // { get; set; }  //�Ѿ� ������

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
        if (target != null && collision.tag != "Finish")
        {
            target.OnDamage(damage);
            Destroy(gameObject);
        }
        
    }




}
