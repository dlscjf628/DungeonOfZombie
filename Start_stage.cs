using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_stage : MonoBehaviour
{
    public Camera cma;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.gameObject.tag == "Right")
        {
            collision.transform.position = new Vector2(collision.transform.position.x + 3.5f, collision.transform.position.y);
            cma.transform.position = new Vector3(cma.transform.position.x + 22, cma.transform.position.y, cma.transform.position.z);
        }
        else if (this.gameObject.tag == "Left")
        {
            collision.transform.position = new Vector2(collision.transform.position.x - 3.5f, collision.transform.position.y);
            cma.transform.position = new Vector3(cma.transform.position.x - 22, cma.transform.position.y, cma.transform.position.z);
        }
        else if (this.gameObject.tag == "Up")
        {
            collision.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y + 3.5f);
            cma.transform.position = new Vector3(cma.transform.position.x, cma.transform.position.y + 10, cma.transform.position.z);
        }
        else if (this.gameObject.tag == "Down")
        {
            collision.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y - 3.5f);
            cma.transform.position = new Vector3(cma.transform.position.x, cma.transform.position.y - 10, cma.transform.position.z);
        }
    }
}
