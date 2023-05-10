using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Townmove : MonoBehaviour
{
    public Camera cam;
    public GameObject[] door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "Market")
            {
                cam.transform.position = new Vector3(-22f, 0, -10f);
                collision.transform.position = new Vector2(-22f, -2.8f);
            }
            else if (this.gameObject.tag == "Dungeon")
            {
                if (collision.gameObject.GetComponent<Player>().D2 && !collision.gameObject.GetComponent<Player>().D3 && !collision.gameObject.GetComponent<Player>().D4)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        door[i].GetComponent<CS_MoveDungeon>().Dungeon1();
                    }
                }
                else if (collision.gameObject.GetComponent<Player>().D2 && collision.gameObject.GetComponent<Player>().D3 && !collision.gameObject.GetComponent<Player>().D4)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        door[i].GetComponent<CS_MoveDungeon>().Dungeon2();
                    }
                }
                else if (collision.gameObject.GetComponent<Player>().D2 && collision.gameObject.GetComponent<Player>().D3 && collision.gameObject.GetComponent<Player>().D4)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        door[i].GetComponent<CS_MoveDungeon>().Dungeon3();
                    }
                }
                cam.transform.position = new Vector3(22f, 0, -10f);
                collision.transform.position = new Vector2(22f, -2.8f);
            }
        }
    }
}
