using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPosition : MonoBehaviour
{
    GameObject player;
    GameObject mouse;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.transform.position = new Vector3(0, 0, 0);
        player.GetComponent<Player>().Mouse = GameObject.Find("Mouse");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
