using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoBossZombieAni : MonoBehaviour
{
    Animator ani;
    public GameObject weapon;

    GameObject zombie;
    GameObject[] specialZombie;
    GameObject viper;
    GameObject viperLocation;

    TwoBossZombie twoBoss;
    BossAttack bossAttack;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        bossAttack = weapon.GetComponent<BossAttack>();
        twoBoss = GetComponentInParent<TwoBossZombie>();
        zombie = transform.parent.GetComponent<TwoBossZombie>().zombie;
        specialZombie = transform.parent.GetComponent<TwoBossZombie>().specialZombie;
        viper = transform.parent.GetComponent<TwoBossZombie>().viper;
        viperLocation = GameObject.Find("ViperLocation");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AttackEnd()
    {
        ani.SetBool("NormalAttack", false);
        bossAttack.hit = true;
    }
    void SkillAttackEnd()
    {
        ani.SetBool("SkillAttack", false);
        Instantiate(viper, viperLocation.transform.position, Quaternion.identity);
        twoBoss.m = false;
    }

    void SkillMagicAttackEnd()
    {
        ani.SetBool("SkillMagic", false);
        for (int i = 0; i < 3; i++)
        {
            float x, y;
            x = Random.Range(-9.0f, 9.0f);
            y = Random.Range(16f, 23f);
            Instantiate(zombie, new Vector3(x, y, 0), Quaternion.identity);
        }
        int r = Random.Range(0, 3);
        float x2, y2;
        x2 = Random.Range(-9.0f, 9.0f);
        y2 = Random.Range(16f, 23f);
        Instantiate(specialZombie[r], new Vector3(x2, y2, 0), Quaternion.identity);

        twoBoss.m = false;
    }
}
