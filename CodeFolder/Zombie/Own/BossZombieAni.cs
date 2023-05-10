using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieAni : MonoBehaviour
{
    Animator ani;
    public GameObject weaponColl;
    public GameObject weapon;

    GameObject boss;
    GameObject bossWeapon;
    GameObject zombie;

    BossAttack bossAttack;
    BossZombieState state;
    BossZombie bosszombie;

    float t;
    public bool b { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        boss = transform.parent.gameObject;
        state = boss.GetComponent<BossZombieState>();
        ani = GetComponent<Animator>();
        bossAttack = weaponColl.GetComponent<BossAttack>();
        bossWeapon = weaponColl.transform.parent.gameObject;
        zombie = boss.GetComponent<BossZombie>().zombie;
        bosszombie = boss.GetComponent<BossZombie>();
    }

    // Update is called once per frame
    void Update()
    {
        if (b)
        {
            t += Time.deltaTime;
        }
    }

    void SkillAttackEnd()
    {
        ani.SetBool("SkillAttack", false);
        bossWeapon.SetActive(false);
        Instantiate(weapon, bossWeapon.transform.position, Quaternion.identity);
        b = true;
        boss.GetComponent<BossZombie>().b = false;

    }
    void NormalAttackEnd()
    {
        ani.SetBool("NormalAttack", false);
        bossAttack.hit = true;
    }
    void SlowAttackEnd()
    {
        ani.SetBool("SlowAttack", false);
        Instantiate(bosszombie.slowBall, bossWeapon.transform.position, Quaternion.identity);
        boss.GetComponent<BossZombie>().b = false;
        boss.GetComponent<BossZombie>().m = false;
    }

    void SkillMagicEnd()
    {
        ani.SetBool("SkillMagic", false);
        for (int i = 0; i < 4; i++)
        {
            float x, y;
            x = Random.Range(35f, 52f);
            y = Random.Range(27f, 32f);
            Instantiate(zombie, new Vector3(x, y, 0), Quaternion.identity);
        }
        boss.GetComponent<BossZombie>().m = false;
        boss.GetComponent<BossZombie>().b = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject w = GameObject.Find("Boss1Weapon(Clone)");
        if (collision.tag == "BossWeapon1" && w.GetComponent<firstBossWeapon>().cnt == 1)
        {
            print("aa");
            Destroy(collision.gameObject);
            bossWeapon.SetActive(true);
            t = 0;
            b = false;
            //state.speed = 3f;
        }
    }

}
