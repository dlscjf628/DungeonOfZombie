using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoBossZombie : MonoBehaviour, GunDamage
{
    BossZombieState state;
    GameObject unit;
    GameObject target;

    public GameObject zombie;
    public GameObject[] specialZombie;
    public GameObject viper;
    public GameObject spawn;
    public GameObject dermis;
    public GameObject chest;
    public GameObject bounschest;

    GameObject bossHp;
    Canvas can;
    Image hpBar;
    float hpMax;

    Animator ani;
    Rigidbody2D rigid;

    float distance;
    float t, t1;

    int r;
    bool b;
    public bool m;

    GameObject can1;

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<BossZombieState>();
        unit = transform.GetChild(0).gameObject;
        ani = unit.GetComponent<Animator>();
        target = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
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

        if (!(Manager.instance != null && Manager.instance.gameOver))
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
        hpBarText();
    }

    void Move()
    {
        rigid.velocity = Vector2.zero;
        if (distance <= 2.6f && t > 1f)
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

        if (rigid.velocity != Vector2.zero && !m)
        {
            ani.SetBool("Run", true);
            r = Random.Range(0, 4000);
            if(r==1 && !b)
            {
                ani.SetBool("Run", false);
                ani.SetBool("SkillMagic", true);
                b = true;
                m = true;
                StartCoroutine(Stop());
            }
            if (r == 2)
            {
                ani.SetBool("Run", false);
                m = true;
                SkillAttack();
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

    void SkillAttack()
    {
        ani.SetBool("SkillAttack", true);
    }

    public virtual void OnDamage(float damage)
    {
        state.hp -= damage;
        print(state.hp);
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
        state.speed = 0;
        StartCoroutine(DieMotion());
        spawn.GetComponent<BossZombieSpawn>().count++;
        dermis.SetActive(true);
        chest.SetActive(true);
        target.GetComponent<Player>().D3 = true;
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

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(5f);
        b = false;
    }

    IEnumerator MagicMotion()
    {
        yield return new WaitForSeconds(5f);
    }



}
