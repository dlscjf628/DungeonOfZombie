using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowZombie : MonoBehaviour, GunDamage
{
    ZombieState state;

    GameObject unit;
    GameObject target;

    public GameObject mineral;
    public GameObject bone;
    public GameObject boneLocation;
    public GameObject spawn;

    Animator ani;
    Rigidbody2D rigid;

    bool b = true;
    int moveCnt;
    float t, t1;
    float x, y;
    float distance = 1; //좀비와 플레이어와의 거리
    public bool coolTime = true;

    GameObject can;

    // Start is called before the first frame update
    void Start()
    {
        unit = transform.GetChild(0).gameObject;
        state = GetComponent<ZombieState>();
        ani = unit.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
        can = GameObject.Find("Canvas");
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

        rigid.velocity = (target.transform.position - transform.position).normalized * state.speed;
        
        if (rigid.velocity.x > 0)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        else if (rigid.velocity.x < 0)
            transform.localScale = new Vector3(2, 2, 1);

        if (coolTime)
        {
            ani.SetBool("Run", true);
            if (t > 2f)
            {
                t = 0;
                coolTime = false;
                Attack();
                StartCoroutine(BoneAttack());
            }
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


    //플레이어에게 데미지를 입을 경우
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


    //좀비가 죽을 경우
    public void Die()
    {
        ani.SetTrigger("Die");
        can.GetComponent<DontDestory>().zombieDeadMusic = true;
        state.speed = 0;
        t = 0;
        StartCoroutine(DieMotion());
        spawn.GetComponent<zombiemove>().count++;
        
    }

    void Attack()
    {
        ani.SetBool("Attack", true);
    }

    IEnumerator DieMotion()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Instantiate(mineral, transform.position, Quaternion.identity);
    }

    IEnumerator MoveRayy()
    {
        yield return new WaitForSeconds(0.25f);
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

    IEnumerator BoneAttack()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(bone, boneLocation.transform.position, Quaternion.identity);
    }

}
