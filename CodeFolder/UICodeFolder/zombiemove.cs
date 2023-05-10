using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiemove : MonoBehaviour
{
    public GameObject GOB;
    public GameObject GOB1;
    public int ZombieNum;
    public int count = 0;
    public int cnt = 1;
    public int ZombieNum2;
    public bool clear = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnZombie(count);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GOB.SetActive(true);
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
    public void SpawnZombie(int _A)
    {
        if(cnt == 1 && _A == ZombieNum)
        {
            GOB1.SetActive(true);
            count = 0;
            cnt++;
        }
        else if (cnt == 2 && _A == ZombieNum2)
        {
            clear = true;
            count = 0;
        }
    }
}
