using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstBossWeapon : MonoBehaviour
{
    GameObject player;
    GameObject boss;

    GunDamage damage;
    Rigidbody2D rigid;
    Animator ani;

    Vector3 v ,v2;
    float speed = 10f;
    float t;
    float dmg = 20f;
    public int cnt;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        player = GameObject.Find("Player");
        damage = player.GetComponent<GunDamage>();
        boss = GameObject.Find("BossStage").gameObject.transform.GetChild(8).gameObject;
        v = player.transform.position;
        v2 = (v - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if (cnt == 0) {
            rigid.velocity = v2 * speed;
        }
        else if(cnt == 1){
            rigid.velocity = (boss.transform.position - transform.position).normalized * speed;
        }
        if (rigid.velocity.x < 0)
        {
            ani.SetTrigger("Left");
        }
        else
        {
            ani.SetTrigger("Right");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie" && t > 0.5f)
        {
            
        }
        else if(collision.tag == "Well")
        {
            cnt = 1;
        }
        else if(collision.tag == "Player")
        {
            damage.OnDamage(dmg);
        }
    }
}
