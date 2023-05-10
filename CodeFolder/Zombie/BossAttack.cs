using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    GameObject player;
    GunDamage damage;

    public bool hit;
    bool hit2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        damage = player.GetComponent<GunDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit && hit2)
        {
            damage.OnDamage(20f);
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
