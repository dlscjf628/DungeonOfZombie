using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieSpawn : MonoBehaviour
{
    public GameObject GOB;
    public bool Clear = false;
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Bossspawn(count);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GOB.SetActive(true);
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
    public void Bossspawn(int _A)
    {
        if (_A == 1)
        {
            Clear = true;
        }
    }
}
