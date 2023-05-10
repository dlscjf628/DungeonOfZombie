using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFire : MonoBehaviour
{

    Rigidbody2D rigid;
    GameObject Player;

    GunDamage damage;

    public float dmg;
    float x, y;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        damage = Player.GetComponent<GunDamage>();
        rigid = GetComponent<Rigidbody2D>();
        x = Random.Range(-9.0f, 9.0f);
        y = Random.Range(-4.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(x, y).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            damage.OnDamage(dmg);
            Destroy(gameObject);
        }
        if (collision.tag == "well")
        {
            Destroy(gameObject);
        }
    }
}
