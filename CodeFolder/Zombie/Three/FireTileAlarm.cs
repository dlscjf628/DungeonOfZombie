using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTileAlarm : MonoBehaviour
{
    SpriteRenderer sr;

    float t;
    bool b;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 0.2f)
        {
            if (!b)
            {
                sr.color = new Color(1, 1, 1, 0);
                b = true;
            }
            else
            {
                sr.color = new Color(1, 1, 1, 1);
                b = false;
            }
            t = 0;
        }
    }
}
