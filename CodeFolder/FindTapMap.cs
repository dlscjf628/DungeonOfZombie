using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindTapMap : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<Player>().map = GameObject.Find("MiniMapCanvas").gameObject.GetComponent<Canvas>().transform.GetChild(1).gameObject.GetComponent<Image>().transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
