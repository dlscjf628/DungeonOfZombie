using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    float mineral;
    float t;
    int cnt;

    public Text log;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 3f)
        {
            log.enabled = false;
        }
        else
            log.enabled = true;
    }

    void ItemGet(string item)
    {
        if (cnt == 5) {
            cnt = 0;
            log.text = "";
        }
        log.text += "\n" + item + "¿ª(∏¶) »πµÊ«ﬂΩ¿¥œ¥Ÿ.";
        cnt++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mineral")
        {
            mineral++;
            ItemGet("±§π∞");
            t = 0;
        }
    }
}
