using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    GameObject player;
    GunDamage damage;
    ZombieState state;

    public bool hit;
    bool hit2;
    bool t;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        damage = player.GetComponent<GunDamage>();
        state = transform.parent.gameObject.GetComponent<ZombieState>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0001f, 0, 0);
        if (hit && hit2 && !t)
        {
            damage.OnDamage(20f);
            t = true;
        }
        hit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hit2 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hit2 = false;
        }
    }
}
