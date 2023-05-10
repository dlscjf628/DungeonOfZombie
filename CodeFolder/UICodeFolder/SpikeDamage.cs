using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Player;
    GunDamage dam;
    float time = 0;
    void Start()
    {
        Player = GameObject.Find("Player");
        dam = Player.GetComponent<GunDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && time >= 3f)
        {
            time = 0;
            Player.GetComponent<Player>().hp -= 5;
            print(Player.GetComponent<Player>().hp);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && time >= 3f)
        {
            time = 0;
            Player.GetComponent<Player>().hp -= 5;
            print(Player.GetComponent<Player>().hp);
        }
    }
}
