using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip invation;
    public AudioClip deongun;
    public AudioClip idleSound;
    public AudioClip deadSound;
    GameObject[] musics;
    // Start is called before the first frame update
    void Awake()
    {
        musics = GameObject.FindGameObjectsWithTag("Music");

        if(musics.Length >= 2)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
   
}
