using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Leftdoor : MonoBehaviour
{
    public GameObject[] SRLeft; // 하위 오브젝트의 SpriteRenderer를 끄기 위함
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
            SRLeft[0].SetActive(false);
            SRLeft[1].SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (spawn.GetComponent<zombiemove>().clear)
        {
            collision.transform.position = new Vector2(collision.transform.position.x + 3.5f, collision.transform.position.y);
            cma.transform.position = new Vector3(cma.transform.position.x + 22, cma.transform.position.y, cma.transform.position.z);
        }
    }
}
