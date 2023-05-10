using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZeroBossZombie : MonoBehaviour, GunDamage
{

    public GameObject head;
    public GameObject body;
    public GameObject dermis;
    public GameObject chest;
    public GameObject bounschest;

    BossZombieState state;
    GameObject unit;
    GameObject target;
    GameObject run;

    Animator ani;
    Rigidbody2D rigid;

    GameObject bossHp;
    Canvas can;
    Image hpBar;
    public GameObject spawn;

    float hpMax;

    float distance;
    float t, t1;
    int r, r2, r3;
    public bool b, m, a;

    GameObject can1;
    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<BossZombieState>();
        unit = transform.GetChild(0).gameObject;
        ani = unit.GetComponent<Animator>();
        target = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        run = transform.GetChild(1).gameObject;
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
            state.speed = 0;
            ani.SetBool("Run", false);
        }
        if (state.speed == 5)
        {
            run.SetActive(true);
        }
        else
        {
            run.SetActive(false);
        }

        hpBarText();

    }

    void Move()
    {
        rigid.velocity = Vector2.zero;
        if (distance <= 2.6f && t > 1f && !m)
        {
            rigid.velocity = Vector2.zero;
            SkillAttack();
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

        if (rigid.velocity != Vector2.zero && !m)
        {
            ani.SetBool("Run", true);
            r = Random.Range(0, 2000);
            r2 = Random.Range(0, 2000);
            r3 = Random.Range(0, 5000);
            if (r == 1 && !b)
            {
                b = true;
                state.speed = 5f;
                StartCoroutine(Speed());
            }
            if (r2 == 2 && !b)
            {
                b = true;
                m = true;
                SkillMagic();
            }
            if(r3 == 3 && !b)
            {
                b = true;
                a = true;
                body.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f, 1);
                head.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f, 1);
                StartCoroutine(Hard());
            }
        }
        else
        {
            ani.SetBool("Run", false);
        }
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
        if (a == true)
            damage /= 2;
        state.hp -= (int)damage;
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
        target.GetComponent<Player>().D1 = true;
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
        m = false;
        a = false;
    }

    IEnumerator Hard()
    {
        yield return new WaitForSeconds(3f);
        b = false;
        a = false;
        body.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
        head.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
    }

    void hpBarText()
    {
        hpBar.fillAmount = state.hp / hpMax;
    }
}

