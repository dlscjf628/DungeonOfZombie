using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCam : MonoBehaviour
{
    public Camera mini;
    public GameObject[] map;
    public GameObject playerImage;
    public GameObject[] mark;

    bool[] visited = new bool[10];

    Vector3 first;
    Vector3 cameraPos;

    int t = 0;
    // Start is called before the first frame update
    void Start()
    {
        first = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos = Camera.main.transform.position;
        if (cameraPos.x > first.x)
        {
            first = cameraPos;
            for(int i=0; i<11; i++)
            {
                if(map[t].transform.position.x + 22 == map[i].transform.position.x && map[t].transform.position.y == map[i].transform.position.y)
                {
                    map[i].SetActive(true);
                    if (i > 0)
                    {
                        visited[i - 1] = true;
                        mark[i - 1].SetActive(false);
                    }
                    t = i;
                    mini.transform.position = new Vector3(mini.transform.position.x + 22, mini.transform.position.y, mini.transform.position.z);
                    playerImage.transform.position = 
                        new Vector3(playerImage.transform.position.x + 22, playerImage.transform.position.y, playerImage.transform.position.z);
                    break;
                }
            }

        }
        else if (cameraPos.x < first.x)
        {
            first = cameraPos;
            for (int i = 0; i < 11; i++)
            {
                if (map[t].transform.position.x - 22 == map[i].transform.position.x && map[t].transform.position.y == map[i].transform.position.y)
                {
                    map[i].SetActive(true);
                    if (i > 0)
                    {
                        visited[i - 1] = true;
                        mark[i - 1].SetActive(false);
                    }
                    t = i;
                    mini.transform.position = new Vector3(mini.transform.position.x - 22, mini.transform.position.y, mini.transform.position.z);
                    playerImage.transform.position =
                        new Vector3(playerImage.transform.position.x - 22, playerImage.transform.position.y, playerImage.transform.position.z);
                    break;
                }
            }

        }
        if (cameraPos.y > first.y)
        {
            first = cameraPos;
            for (int i = 0; i < 11; i++)
            {
                if (map[t].transform.position.y + 17 == map[i].transform.position.y && map[t].transform.position.x == map[i].transform.position.x)
                {
                    map[i].SetActive(true);
                    if (i > 0)
                    {
                        visited[i - 1] = true;
                        mark[i - 1].SetActive(false);
                    }
                    t = i;
                    mini.transform.position = new Vector3(mini.transform.position.x, mini.transform.position.y + 17, mini.transform.position.z);
                    playerImage.transform.position =
                        new Vector3(playerImage.transform.position.x, playerImage.transform.position.y + 17, playerImage.transform.position.z);
                    break;
                }
            }

        }
        else if (cameraPos.y < first.y)
        {
            first = cameraPos;
            for (int i = 0; i < 11; i++)
            {
                if (map[t].transform.position.y - 17 == map[i].transform.position.y && map[t].transform.position.x == map[i].transform.position.x)
                {
                    map[i].SetActive(true);
                    if (i > 0)
                    {
                        visited[i - 1] = true;
                        mark[i - 1].SetActive(false);
                    }
                    t = i;
                    mini.transform.position = new Vector3(mini.transform.position.x, mini.transform.position.y - 17, mini.transform.position.z);
                    playerImage.transform.position =
                        new Vector3(playerImage.transform.position.x, playerImage.transform.position.y - 17, playerImage.transform.position.z);
                    break;
                }
            }

        }

        for(int i=0; i<10; i++)
        {
            if (map[t].transform.position.x - 22 == mark[i].transform.position.x && map[t].transform.position.y == mark[i].transform.position.y && !visited[i])
                mark[i].SetActive(true);
            if (map[t].transform.position.x + 22 == mark[i].transform.position.x && map[t].transform.position.y == mark[i].transform.position.y && !visited[i])
                mark[i].SetActive(true);
            if (map[t].transform.position.y - 17 == mark[i].transform.position.y && map[t].transform.position.x == mark[i].transform.position.x && !visited[i])
                mark[i].SetActive(true);
            if (map[t].transform.position.y + 17 == mark[i].transform.position.y && map[t].transform.position.x == mark[i].transform.position.x && !visited[i])
                mark[i].SetActive(true);
        }

    }
}
