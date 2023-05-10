using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_NewGame : MonoBehaviour
{
    public GameObject OptionPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SceneLoad(string Scene)
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void GameOption()
    {
        OptionPanel.SetActive(true);
    }
    public void GameLoad()
    {
        
    }
    public void GameOption2()
    {
        OptionPanel.SetActive(false);
    }
    public void Gameclear()
    {
        SceneManager.LoadScene("MainStartUI");
    }
}
