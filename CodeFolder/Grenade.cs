using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject boom;

    Vector3 mouse;
    Rigidbody2D rigid;

    float damage = 10f;
    int cnt = 0;
    bool dmg;

    BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if (cnt == 0)
        {
            rigid.velocity = mouse.normalized * 15f;
            StartCoroutine(Boom());
            cnt++;
        }
    }

    IEnumerator Boom()
    {
        yield return new WaitForSeconds(3f);
        boom.SetActive(true);
        dmg = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie" && dmg)
        {
            GunDamage target = collision.GetComponent<GunDamage>();
            target.OnDamage(damage);
        }
    }

    

}
