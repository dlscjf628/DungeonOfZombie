using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTile : MonoBehaviour
{
    SpriteRenderer sr;

    GunDamage damage;

    public float dmg = 10f;
    bool b = true;
    bool hit = false;
    float a;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        damage = GameObject.Find("Player").gameObject.GetComponent<GunDamage>();
        sr.color = new Color(1, 1, 1, a);
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (b)
        {
            StartCoroutine(TransUp());
            b = false;
        }
    }

    IEnumerator TransUp()
    {
        a += 0.1f;
        sr.color = new Color(1, 1, 1, a);
        yield return new WaitForSeconds(0.1f);

        if (a < 1f) StartCoroutine(TransUp());
        else hit = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && hit)
        {
            damage.OnDamage(dmg);
            Destroy(gameObject);
        }
    }
}
