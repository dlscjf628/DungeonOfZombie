using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Downdoor : MonoBehaviour
{
    public GameObject[] SRDown; // 하위 오브젝트의 SpriteRenderer를 끄기 위함
    public Camera cma;
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn.GetComponent<zombiemove>().clear)
        {
            SRDown[0].SetActive(false);
            SRDown[1].SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (spawn.GetComponent<zombiemove>().clear)
        {
            collision.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y - 3.5f);
            cma.transform.position = new Vector3(cma.transform.position.x, cma.transform.position.y - 10, cma.transform.position.z);
        }
    }
}
