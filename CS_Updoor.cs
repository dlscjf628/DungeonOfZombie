using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Updoor : MonoBehaviour
{
    public GameObject[] SRUp; // 하위 오브젝트의 SpriteRenderer를 끄기 위함
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
            //클리어 시 열리게 함
            SRUp[0].SetActive(false);
            SRUp[1].SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (spawn.GetComponent<zombiemove>().clear)
        {
            // 부딪히면 캐릭터 위치 이동
            collision.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y + 3.5f);
            cma.transform.position = new Vector3(cma.transform.position.x, cma.transform.position.y + 10, cma.transform.position.z);
        }
    }
}
