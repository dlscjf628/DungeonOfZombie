using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyShopButton : MonoBehaviour
{
    public List<Item> item;
    public GameObject[] Bullet;
    public GameObject ak;
    public GameObject shotgun;
    public GameObject sinper;
    public GameObject Gun;
    int bulletcount = 0;
    int Weaponcount = 0;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Buybullet()
    {
        if (item[7].num >= 20)
        {
            item[0].num = 99999999;
            item[1].num += 10;
            item[2].num += 10;
            item[3].num += 10;
            item[7].num -= 20;
        }
    }
    public void BuyGrenade()
    {
        if (item[7].num >= 10)
        {
            item[4].num += 5;
            item[7].num -= 10;
        }
    }
    public void BuyHP()
    {
        if (item[7].num >= 25)
        {
            item[5].num += 5;
            item[7].num -= 25;
        }
    }
    public void BuyMaxHP()
    {
        if (item[7].num >= 40)
        {
            item[6].num += 1;
            item[7].num -= 40;
        }
    }
    public void UpGradeBullet()
    {
        if (item[7].num >= 100 && bulletcount < 5)
        {
            ak.GetComponent<Weapon>().magazine += 5;
            shotgun.GetComponent<ShotGun>().magazine += 5;
            sinper.GetComponent<Sniper>().magazine += 5;
            Gun.GetComponent<Gun>().magazine += 5;
            item[7].num -= 100;
            bulletcount++;
        }
    }
    public void UpGradeDamage()
    {
        if (item[7].num >= 100 && Weaponcount < 5)
        {
            for (int i = 0; i < 4; i++)
            {
                Bullet[i].GetComponent<Bulliet>().damage += 5;
            }
            item[7].num -= 100;
            Weaponcount++;
        }
    }
}
