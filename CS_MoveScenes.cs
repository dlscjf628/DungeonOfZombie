using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_MoveScenes : MonoBehaviour
{
    public GameObject spawn;
    public Camera cam;
    public GameObject[] Door;
    GameObject musicOption;
    AudioClip music;
    // Start is called before the first frame update
    void Start()
    {
        musicOption = GameObject.Find("MusicOption");
        music = musicOption.GetComponent<BackGroundMusic>().invation;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn.GetComponent<BossZombieSpawn>().Clear)
        {
            Door[0].SetActive(false);
            Door[1].SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (spawn.GetComponent<BossZombieSpawn>().Clear && this.gameObject.tag == "BossMoveScene")
        {
            musicOption.GetComponent<AudioSource>().clip = music;
            musicOption.GetComponent<AudioSource>().Play();
            SceneManager.LoadScene("Town");
        }
        else if (spawn.GetComponent<BossZombieSpawn>().Clear && this.gameObject.tag == "BossDoor")
        {
            if (Door[0].gameObject.tag == "Up")
            {
                collision.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y + 3.5f);
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 10, cam.transform.position.z);
            }
            else if (Door[0].gameObject.tag == "Down")
            {
                collision.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y - 3.5f);
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 10, cam.transform.position.z);
            }
            else if (Door[0].gameObject.tag == "Right")
            {
                collision.transform.position = new Vector2(collision.transform.position.x + 3.5f, collision.transform.position.y);
                cam.transform.position = new Vector3(cam.transform.position.x + 22, cam.transform.position.y, cam.transform.position.z);
            }
            else if (Door[0].gameObject.tag == "Left")
            {
                collision.transform.position = new Vector2(collision.transform.position.x - 3.5f, collision.transform.position.y);
                cam.transform.position = new Vector3(cam.transform.position.x - 22, cam.transform.position.y, cam.transform.position.z);
            }
        }
    }
}
