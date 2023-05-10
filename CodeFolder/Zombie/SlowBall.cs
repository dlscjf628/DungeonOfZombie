using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBall : MonoBehaviour
{
    GameObject player;

    Rigidbody2D rigid;

    GunDamage damage;
    Player play;

    public float dmg;

    public float speed;
    Vector3 v, v2;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        play = player.GetComponent<Player>();
        damage = player.GetComponent<GunDamage>();
        v = player.transform.position;
        v2 = (v - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = v2 * speed;
        if (rigid.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            damage.OnDamage(dmg);
            play.slow = true;
            Destroy(gameObject);
        }
        if (collision.tag == "Well")
        {
            Destroy(gameObject);
        }
    }
}