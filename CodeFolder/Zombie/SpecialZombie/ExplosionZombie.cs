using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionZombie : MonoBehaviour, GunDamage
{
    ZombieState state;
    Explosion explosion;

    GameObject unit;
    GameObject boom;
    GameObject target;
    GameObject ex;

    public GameObject mineral;
    public GameObject head;
    public GameObject body;
    public GameObject spawn;

    Animator ani;
    Rigidbody2D rigid;

    bool b = true;
    int moveCnt;
    float t, t1;
    float x, y;
    float distance = 1; //����� �÷��̾���� �Ÿ�

    GameObject can1;

    // Start is called before the first frame update
    void Start()
    {
        unit = transform.GetChild(0).gameObject;
        boom = transform.GetChild(1).gameObject;
        explosion = boom.GetComponent<Explosion>();
        ex = transform.GetChild(1).gameObject;
        state = GetComponent<ZombieState>();
        ani = unit.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
        can1 = GameObject.Find("Canvas");
        t = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Manager.instance != null && Manager.instance.gameOver))
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
            t += Time.deltaTime;
            t1 += Time.deltaTime;
            if (t1 > 2f)
            {
                t1 = 0;
                Manager.instance.zombieMusic = true;
            }
            if (b)
            {
                Move();
            }
            else
            {
                MoveWall();
            }
            MoveRay();
        }
        else
        {
            rigid.velocity = Vector2.zero;
            ani.SetBool("Run", false);
        }

    }

    void Move()
    {
        rigid.velocity = Vector2.zero;
        if (distance <= 2f)
        {
            StartCoroutine(ColorChange1());
            StartCoroutine(Explosion());
        }
        else if (distance > 1.5f)
        {
            rigid.velocity = (target.transform.position - transform.position).normalized * state.speed;
        }

        if (rigid.velocity.x > 0)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        else if (rigid.velocity.x < 0)
            transform.localScale = new Vector3(2, 2, 1);

        if (rigid.velocity != Vector2.zero)
        {
            ani.SetBool("Run", true);
        }
        else
            ani.SetBool("Run", false);
    }

    void MoveRay()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + 0.5f);
        Vector2 pos1 = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D rayUp = Physics2D.Raycast(pos, Vector2.up, 1f, LayerMask.GetMask("Wall"));
        RaycastHit2D rayDown = Physics2D.Raycast(pos, Vector2.down, 1f, LayerMask.GetMask("Wall"));
        RaycastHit2D rayLeft = Physics2D.Raycast(pos, Vector2.left, 1f, LayerMask.GetMask("Wall"));
        RaycastHit2D rayRight = Physics2D.Raycast(pos, Vector2.right, 1f, LayerMask.GetMask("Wall"));

        RaycastHit2D rayUp1 = Physics2D.Raycast(pos1, Vector2.up, 1f, LayerMask.GetMask("Wall"));
        RaycastHit2D rayDown1 = Physics2D.Raycast(pos1, Vector2.down, 1f, LayerMask.GetMask("Wall"));
        RaycastHit2D rayLeft1 = Physics2D.Raycast(pos1, Vector2.left, 1f, LayerMask.GetMask("Wall"));
        RaycastHit2D rayRight1 = Physics2D.Raycast(pos1, Vector2.right, 1f, LayerMask.GetMask("Wall"));

        if (rayUp.collider != null || rayUp1.collider != null)
        {
            x = rigid.velocity.x;
            y = rigid.velocity.y;
            moveCnt = 1;
            b = false;
        }
        else if (rayDown.collider != null || rayDown1.collider != null)
        {
            x = rigid.velocity.x;
            y = rigid.velocity.y;
            moveCnt = 2;
            b = false;
        }
        else if (rayLeft.collider != null || rayLeft1.collider != null)
        {
            x = rigid.velocity.x;
            y = rigid.velocity.y;
            moveCnt = 3;
            b = false;
        }
        else if (rayRight.collider != null || rayRight1.collider != null)
        {
            x = rigid.velocity.x;
            y = rigid.velocity.y;
            moveCnt = 4;
            b = false;
        }
        else
        {
            StartCoroutine(MoveRayy());
        }
    }

    void MoveWall()
    {
        if (moveCnt == 1)
        {
            if (x > 0)
            {
                rigid.velocity = Vector2.right * state.speed;
            }
            else
            {
                rigid.velocity = Vector2.left * state.speed;
            }
        }
        else if (moveCnt == 2)
        {
            if (x > 0)
            {
                rigid.velocity = Vector2.right * state.speed;
            }
            else
            {
                rigid.velocity = Vector2.left * state.speed;
            }
        }
        else if (moveCnt == 3)
        {
            if (y > 0)
            {
                rigid.velocity = Vector2.up * state.speed;
            }
            else
            {
                rigid.velocity = Vector2.down * state.speed;
            }
        }
        else if (moveCnt == 4)
        {
            if (y > 0)
            {
                rigid.velocity = Vector2.up * state.speed;
            }
            else
            {
                rigid.velocity = Vector2.down * state.speed;
            }
        }
    }


    //�÷��̾�� �������� ���� ���
    public virtual void OnDamage(float damage)
    {
        state.hp -= damage;
        StartCoroutine(Back());
        if (state.hp <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            unit.GetComponent<CapsuleCollider2D>().enabled = false;
            Die();
        }
    }


    //���� ���� ���
    public void Die()
    {
        ani.SetTrigger("Die");
        can1.GetComponent<DontDestory>().zombieDeadMusic = true;
        state.speed = 0;
        StartCoroutine(DieMotion());
        spawn.GetComponent<zombiemove>().count++;
    }

    IEnumerator DieMotion()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        Instantiate(mineral, transform.position, Quaternion.identity);
    }

    IEnumerator MoveRayy()
    {
        yield return new WaitForSeconds(0.5f);
        b = true;
    }

    IEnumerator Back()
    {
        float ctime = 0;
        while (ctime < 0.3f)
        {
            transform.Translate(-(target.transform.position - transform.position).normalized * 0.05f);
            ctime += Time.deltaTime;
        }
        yield return null;
    }

    IEnumerator ColorChange1()
    {
        head.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
        body.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ColorChange2());
    }

    IEnumerator ColorChange2()
    {
        head.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        body.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ColorChange1());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(1f);
        explosion.hit = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        unit.GetComponent<CapsuleCollider2D>().enabled = false;
        ex.SetActive(true);
        state.speed = 0;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        spawn.GetComponent<zombiemove>().count++;
    }

}

