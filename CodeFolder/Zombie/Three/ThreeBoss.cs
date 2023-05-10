using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeBoss : MonoBehaviour, GunDamage
{
    public GameObject fireBall;
    public GameObject randomFrieBall;
    public GameObject zombie;
    public GameObject fireTile;
    public GameObject fireAlarm;
    public GameObject spawn;
    public GameObject chest;
    public GameObject bounschest;
    public GameObject dermis;
    GameObject target;
    GameObject bossAni;
    GameObject fireBallLocation;

    ThreeBossState state;
    ThreeBoossAni ani;
    RandomFire rf;
    FireBall f;

    GameObject can1;

    GameObject bossHp;
    Canvas can;
    Image hpBar;
    float hpMax;

    float t;
    float t2, t1;
    // Start is called before the first frame update
    void Start()
    {
        bossAni = transform.GetChild(0).gameObject;
        ani = bossAni.GetComponent<ThreeBoossAni>();
        fireBallLocation = transform.GetChild(1).gameObject;
        state = GetComponent<ThreeBossState>();
        rf = randomFrieBall.GetComponent<RandomFire>();
        f = fireBall.GetComponent<FireBall>();
        can1 = GameObject.Find("Canvas");
        target = GameObject.Find("Player");
        can = GameObject.Find("UI").gameObject.GetComponent<Canvas>();
        hpBar = can.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        bossHp = hpBar.transform.parent.gameObject;
        hpMax = state.hp;
        can1 = GameObject.Find("Canvas");
        bossHp.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Manager.instance != null && Manager.instance.gameOver))
        {
            t += Time.deltaTime;
            t2 += Time.deltaTime;
            t1 += Time.deltaTime;
            if (t1 > 2f)
            {
                t1 = 0;
                Manager.instance.zombieMusic = true;
            }
            if (t > 1f)
            {
                t = 0;
                BowAttack();
            }
            else
            {
                int r = Random.Range(0, 1000);
                if (r == 1)
                {
                    t = 0;
                    MagicAttack();
                }
            }
            if (t2 > 7f)
            {
                t2 = 0;
                ZombieAttack();
                FireTileAttack();
            }
        }
        hpBarText();
    }


    void BowAttack()
    {
        ani.BowAttack();
        f.dmg = state.damage;
        Instantiate(fireBall, fireBallLocation.transform.position, Quaternion.identity);
    }
    void MagicAttack()
    {
        ani.MagicAttack();
        rf.dmg = state.damage;
        for (int i = 0; i < 6; i++)
        {
            Instantiate(randomFrieBall, fireBallLocation.transform.position, Quaternion.identity);
        }

    }
    void ZombieAttack()
    {
        ani.ZombieAttack();
        for (int i = 0; i < 4; i++)
        {
            float x, y;
            x = Random.Range(35.0f, 53.0f);
            y = Random.Range(-7.0f, -13.0f);
            Instantiate(zombie, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    void FireTileAttack()
    {
        for (int i = 0; i < 4; i++)
        {
            float x, y;
            x = Random.Range(35f, 53f);
            y = Random.Range(-13f, -7f);
            StartCoroutine(FireTileTrigger(x, y, 0));
        }
    }

    public virtual void OnDamage(float damage)
    {
        state.hp -= damage;
        print(state.hp);
        if (state.hp <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            bossAni.GetComponent<CapsuleCollider2D>().enabled = false;
            Die();
        }
    }

    void Die()
    {
        ani.Die();
        can1.GetComponent<DontDestory>().zombieDeadMusic = true;
        StartCoroutine(DieMotion());
        spawn.GetComponent<BossZombieSpawn>().count++;
        dermis.SetActive(true);
        chest.SetActive(true);
        target.GetComponent<Player>().D4 = true;
    }

    void hpBarText()
    {
        hpBar.fillAmount = state.hp / hpMax;
    }
    IEnumerator DieMotion()
    {
        yield return new WaitForSeconds(1f);
        bounschest.SetActive(true);
        Destroy(chest);
        Destroy(gameObject);
    }

    IEnumerator FireTileTrigger(float x, float y, int c)
    {
        Instantiate(fireAlarm, new Vector3(x, y, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        Instantiate(fireTile, new Vector3(x, y, 0), Quaternion.identity);
    }

}