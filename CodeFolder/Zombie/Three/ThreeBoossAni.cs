using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeBoossAni : MonoBehaviour
{
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BowAttack()
    {
        ani.SetBool("Bow", true);
    }
    public void BowAttackEnd()
    {
        ani.SetBool("Bow", false);
    }
    public void MagicAttack()
    {
        ani.SetBool("Magic", true);
    }
    public void MagicAttackEnd()
    {
        ani.SetBool("Magic", false);
    }
    public void ZombieAttack()
    {
        ani.SetBool("Magic", true);
    }
    public void Die()
    {
        ani.SetTrigger("Die");
    }

}
