using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopopen : MonoBehaviour
{
    GameObject WeaponShop;
    GameObject Portionshop;
    GameObject Inven;
    Player Player;

    

    // Start is called before the first frame update
    void Start()
    {
        Inven = GameObject.Find("Canvas").gameObject.GetComponent<Canvas>().transform.GetChild(0).gameObject;
        WeaponShop = GameObject.Find("Canvas").gameObject.GetComponent<Canvas>().transform.GetChild(1).gameObject;
        Portionshop = GameObject.Find("Canvas").gameObject.GetComponent<Canvas>().transform.GetChild(2).gameObject;
        Player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "Weaponer")
            {
                WeaponShop.SetActive(true);
                Inven.SetActive(true);
                Player.UIOpen = false;
            }
            else if (this.gameObject.tag == "Portioner")
            {
                Portionshop.SetActive(true);
                Inven.SetActive(true);
                Player.UIOpen = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "Weaponer")
            {
                WeaponShop.SetActive(false);
                Inven.SetActive(false);
                Player.UIOpen = true;
            }
            else if (this.gameObject.tag == "Portioner")
            {
                Portionshop.SetActive(false);
                Inven.SetActive(false);
                Player.UIOpen = true;
            }
        }
    }
}
