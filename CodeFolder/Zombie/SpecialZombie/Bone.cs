using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    GameObject player;

    Rigidbody2D rigid;
    Animator ani;

    GunDamage damage;

    public float dmg;
    public float speed;
    float t;
    bool b;
    Vector3 v;
    Vector3 v2;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        player = GameObject.Find("Player");
        damage = player.GetComponent<GunDamage>();
        v = player.transform.position;
        v2 = (v - transform.position).normalized;
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {

        rigid.velocity = v2 * speed;

        if (rigid.velocity.x < 0 && !b)
        {
            ani.SetTrigger("Left");
            b = true;
        }
        else if (rigid.velocity.x >= 0 && !b)
        {
            ani.SetTrigger("Right");
            b = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            damage.OnDamage(dmg);
            Destroy(gameObject);
        }
        else if (collision.tag == "Well")
        {
            Destroy(gameObject);
        }
    }
}

