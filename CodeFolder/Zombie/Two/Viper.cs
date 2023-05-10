using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viper : MonoBehaviour
{
    GameObject player;

    Rigidbody2D rigid;

    GunDamage damage;

    public float dmg;
    public float speed;
    Vector3 v, v2;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        damage = player.GetComponent<GunDamage>();
        v = player.transform.position;
        v2 = (v - transform.position).normalized;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = v2 * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            damage.OnDamage(dmg);
            player.GetComponent<Player>().poison = true;
            Destroy(gameObject);
        }
        if (collision.tag == "Well")
        {
            Destroy(gameObject);
        }
    }
}
