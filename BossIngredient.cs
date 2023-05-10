using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIngredient : MonoBehaviour
{
    public Item One;
    public Item Two;
    public Item Three;
    public Item Four;
    GameObject Inven;
    // Start is called before the first frame update
    void Start()
    {
        Inven = GameObject.Find("Canvas").gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.tag == "One")
        {
            One.num++;
            Inven.GetComponent<Inventory>().AddItem(One);
            this.gameObject.SetActive(false);
        }
        else if (this.gameObject.tag == "Two")
        {
            Two.num++;
            Inven.GetComponent<Inventory>().AddItem(Two);
            this.gameObject.SetActive(false);
        }
        else if (this.gameObject.tag == "Three")
        {
            Three.num++;
            Inven.GetComponent<Inventory>().AddItem(Three);
            this.gameObject.SetActive(false);
        }
        else if (this.gameObject.tag == "Four")
        {
            Four.num++;
            Inven.GetComponent<Inventory>().AddItem(Four);
            this.gameObject.SetActive(false);
        }
    }
}
