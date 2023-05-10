using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_MoveDungeon : MonoBehaviour
{
    GameObject musicOption;
    AudioClip music;
    public Sprite[] abc = new Sprite[2];
    GameObject player;
    public GameObject[] door;
    // Start is called before the first frame update
    void Start()
    {
        musicOption = GameObject.Find("MusicOption");
        music = musicOption.GetComponent<BackGroundMusic>().deongun;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        musicOption.GetComponent<AudioSource>().clip = music;
        musicOption.GetComponent<AudioSource>().Play();
        if (this.gameObject.tag == "Dungeon1")
        {
            SceneManager.LoadScene("Dungeon1");
        }
        else if (this.gameObject.tag == "Dungeon2")
        {
            SceneManager.LoadScene("Dungeon2");
        }
        else if (this.gameObject.tag == "Dungeon3")
        {
            SceneManager.LoadScene("Dungeon3");
        }
    }
    public void Dungeon1()
    {
        door[0].GetComponent<SpriteRenderer>().sprite = abc[0];
        door[0].GetComponent<BoxCollider2D>().isTrigger = false;
        door[1].GetComponent<SpriteRenderer>().sprite = abc[1];
        door[1].GetComponent<BoxCollider2D>().isTrigger = true;
        door[2].GetComponent<SpriteRenderer>().sprite = abc[0];
        door[2].GetComponent<BoxCollider2D>().isTrigger = false;
    }
    public void Dungeon2()
    {
        door[0].GetComponent<SpriteRenderer>().sprite = abc[0];
        door[0].GetComponent<BoxCollider2D>().isTrigger = false;
        door[1].GetComponent<SpriteRenderer>().sprite = abc[0];
        door[1].GetComponent<BoxCollider2D>().isTrigger = false;
        door[2].GetComponent<SpriteRenderer>().sprite = abc[1];
        door[2].GetComponent<BoxCollider2D>().isTrigger = true;
    }
    public void Dungeon3()
    {
        door[0].GetComponent<SpriteRenderer>().sprite = abc[0];
        door[0].GetComponent<BoxCollider2D>().isTrigger = false;
        door[1].GetComponent<SpriteRenderer>().sprite = abc[0];
        door[1].GetComponent<BoxCollider2D>().isTrigger = false;
        door[2].GetComponent<SpriteRenderer>().sprite = abc[0];
        door[2].GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
