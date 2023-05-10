using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardZombieAni : MonoBehaviour
{
    Animator ani;
    GameObject attack;
    public bool hit;
    public bool hit2;

    BossAttack bossAttack;
    // Start is called before the first frame update
    void Start()
    {
        attack = transform.GetChild(2).gameObject;
        ani = GetComponent<Animator>();
        bossAttack = attack.GetComponent<BossAttack>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Idle()
    {
        ani.SetBool("Attack", false);
        bossAttack.hit = true;
    }
}
