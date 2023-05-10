using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroBossZombeAni : MonoBehaviour
{
    Animator ani;
    public GameObject weapon;
    public GameObject zombie;
    GameObject boss;
    ZeroBossZombie zero;
    BossAttack bossAttack;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        bossAttack = weapon.GetComponent<BossAttack>();
        boss = transform.parent.gameObject;
        zero = boss.GetComponent<ZeroBossZombie>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SkillAttackEnd()
    {
        ani.SetBool("SkillAttack", false);
        bossAttack.hit = true;
    }

    void SkillMagicEnd()
    {
        ani.SetBool("SkillMagic", false);
        for (int i = 0; i < 4; i++)
        {
            float x, y;
            x = Random.Range(58f, 74f);
            y = Random.Range(7f, 13f);
            Instantiate(zombie, new Vector3(x, y, 0), Quaternion.identity);
        }
        zero.b = false;
        zero.m = false;
        zero.a = false;
    }

}
