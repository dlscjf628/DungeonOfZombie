using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestory : MonoBehaviour
{
    GameObject musicOption;
    BackGroundMusic backMusic;
    AudioSource ad;
    public bool zombieDeadMusic;
    // Start is called before the first frame update
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

    void Music()
    {
        if (zombieDeadMusic)
        {
            ad.clip = backMusic.deadSound;
            ad.Play();
            zombieDeadMusic = false;
        }
    }
}
