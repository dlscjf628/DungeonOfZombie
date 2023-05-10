using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowZombieAni : MonoBehaviour
{
    Animator ani;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AttackEnd()
    {
        ani.SetBool("Attack", false);
        transform.parent.gameObject.GetComponent<ThrowZombie>().coolTime = true;
    }
}
