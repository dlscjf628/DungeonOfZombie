using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_Laber : MonoBehaviour
{
    public Text text;
    public GameObject G;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        text.gameObject.SetActive(true);
        G.SetActive(true);
        player.Laberis = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        text.gameObject.SetActive(false);
        G.SetActive(false);
        player.Laberis = false;
        player.panel.SetActive(false);
        player.Mouse.SetActive(true);
        player.UIOpen = true;
    }
}
