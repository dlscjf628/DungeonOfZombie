using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBulliet : MonoBehaviour
{
    float speed = 8f; //총알 속도
    public float damage; // { get; set; }  //총알 데미지

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f); // 총알 발사 후 5초뒤 삭제
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }


    //총알이 좀비에게 맞거나 벽에 맞을 경우
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GunDamage target = collision.GetComponent<GunDamage>();
        if (collision.tag == "Zombie")
        {
            target.OnDamage(damage);
            Destroy(gameObject);
        }

    }

}

