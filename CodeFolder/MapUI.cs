using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public Transform left;
    public Transform right;
    public Transform top;
    public Transform bottom;

    public Image minimapImage;
    public Image minimapPlayer;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        var inst = Instantiate(minimapImage.material);
        //minimapImage.material = inst;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 mapArea = new Vector2(Vector3.Distance(left.position, right.position), Vector3.Distance(bottom.position, top.position));
        Vector2 mapArea = new Vector2(88f, 50f);
        Vector2 charPos = new Vector2(Vector3.Distance(new Vector3(left.position.x, 0f, 0f), new Vector3(player.transform.position.x, 0f, 0f)), 
            Vector3.Distance(new Vector3(0f, bottom.position.y, 0f), new Vector3(0f, player.transform.position.y, 0f)));
        Vector2 normalPos = new Vector2(charPos.x / mapArea.x, charPos.y / mapArea.y);

        minimapPlayer.rectTransform.anchoredPosition = 
            new Vector2(minimapImage.rectTransform.sizeDelta.x * normalPos.x, minimapImage.rectTransform.sizeDelta.y * normalPos.y);
 

    }
}
