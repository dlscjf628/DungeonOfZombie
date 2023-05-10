using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<Manager>();
            }
            return _instance;
        }
    }

    static Manager _instance;

    public bool gameOver { get; set; } //게임 오버 상태

    GameObject musicOption;
    BackGroundMusic backMusic;
    AudioSource ad;
    public bool zombieMusic;

    void Start()
    {
        ad = GetComponent<AudioSource>();
        musicOption = GameObject.Find("MusicOption");
        backMusic = musicOption.GetComponent<BackGroundMusic>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Music();
    }

    public void PlayerDie()
    {
        gameOver = true;
        Destroy(gameObject, 0.1f);
        SceneManager.LoadScene("GameOver");
    }
    public void Ending()
    {
        gameOver = true;
        Destroy(gameObject, 0.1f);
        SceneManager.LoadScene("Ending");
    }
    public void Return()
    {
        gameOver = true;
        Destroy(gameObject, 0.1f);
        SceneManager.LoadScene("Tutorial");
    }
    void Music()
    {
        if (zombieMusic)
        {
            ad.clip = backMusic.idleSound;
            ad.Play();
            zombieMusic = false;
        }
    }

}
