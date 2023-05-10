using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopExit : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject Portion;
    public GameObject Inventroy;
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
    public void WeaponExit()
    {
        Weapon.SetActive(false);
    }
    public void PortionExit1()
    {
        Portion.SetActive(false);
    }
    public void InventoryExit()
    {
        Inventroy.SetActive(false);
        player.UIOpen = true;
    }
}
