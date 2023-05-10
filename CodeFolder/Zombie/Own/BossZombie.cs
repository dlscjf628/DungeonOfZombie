using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossZombie : MonoBehaviour, GunDamage
{
    BossZombieState state;
    GameObject unit;
    GameObject target;

    BossZombieAni bossZombieAni;
    Animator ani;
    Rigidbody2D rigid;

    public GameObject zombie;
    public GameObject slowBall;
    public GameObject dermis;
    public GameObject spawn;
    public GameObject bounschest;
    public GameObject chest;

    GameObject bossHp;
    Canvas can;
    Image hpBar;
    float hpMax;

    GameObject can1;

    float distance;
    float t, t1;
    int r, r2;
    public bool b, m;
    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<BossZombieState>();
        unit = transform.GetChild(0).gameObject;
        ani = unit.GetComponent<Animator>();
        target = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        bossZombieAni = unit.GetComponent<BossZombieAni>();
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
        t += Time.deltaTime;

        if (!(Manager.instance != null && Manager.instance.gameOver) || state.hp > 0)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
            t1 += Time.deltaTime;
            if (t1 > 2f)
            {
                t1 = 0;
                Manager.instance.zombieMusic = true;
            }
            Move();
        }
        else
        {
            rigid.velocity = Vector2.zero;
            ani.SetBool("Run", false);

        }
        hpBarText();
    }

    void Move()
    {
        rigid.velocity = Vector2.zero;
        if (distance <= 2.6f && t > 1f && !bossZombieAni.b && !m)
        {
            rigid.velocity = Vector2.zero;
            Attack();
            t = 0;
        }
        else if (distance > 2.6f)
        {
            rigid.velocity = (target.transform.position - transform.position).normalized * state.speed;
        }

        if (rigid.velocity.x > 0)
        {
            transform.localScale = new Vector3(-3, 3, 1);
        }
        else if (rigid.velocity.x < 0)
            transform.localScale = new Vector3(3, 3, 1);

        if (rigid.velocity != Vector2.zero && !b)
        {
            ani.SetBool("Run", true);
            r = Random.Range(0, 1000);
            r2 = Random.Range(0, 5000);
            if (r == 2 && !bossZombieAni.b && !b)
            {
                b = true;
                SkillAttack();
            }
            if (r2 == 2 && !b)
            {
                m = true;
                b = true;
                SkillMagic();
            }
            else if (r2 == 3 && !b)
            {
                SlowAttack();
                b = true;
                m = true;
            }
        }
        else
        {
            ani.SetBool("Run", false);
        }
    }

    void Attack()
    {
        ani.SetBool("NormalAttack", true);
    }
    void SlowAttack()
    {
        ani.SetBool("SlowAttack", true);
    }
    void SkillAttack()
    {
        ani.SetBool("SkillAttack", true);
    }
    void SkillMagic()
    {
        ani.SetBool("SkillMagic", true);
    }

    public virtual void OnDamage(float damage)
    {
        state.hp -= damage;
        if (state.hp <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            unit.GetComponent<CapsuleCollider2D>().enabled = false;
            Die();
        }
    }

    void Die()
    {
        ani.SetTrigger("Die");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        can1.GetComponent<DontDestory>().zombieDeadMusic = true;
        StartCoroutine(DieMotion());
        spawn.GetComponent<BossZombieSpawn>().count++;
        dermis.SetActive(true);
        chest.SetActive(true);
        target.GetComponent<Player>().D2 = true;
    }

    IEnumerator DieMotion()
    {
        yield return new WaitForSeconds(1f);
        bounschest.SetActive(true);
        Destroy(chest);
        Destroy(gameObject);
    }
    IEnumerator Speed()
    {
        yield return new WaitForSeconds(3f);
        state.speed = 3;
        b = false;
    }

    void hpBarText()
    {
        hpBar.fillAmount = state.hp / hpMax;
    }

}
