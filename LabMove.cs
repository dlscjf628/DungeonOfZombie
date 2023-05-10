using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMove : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cam.transform.position = new Vector3(0, 0, -10f);
            collision.transform.position = new Vector2(0, -3.5f);
        }
    }
}
