using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveLoda : MonoBehaviour
{

    public void Start()
    {
        DataManager.Instance.LoadGameData();
    }
    public void OnApplicationQuit()
    {
        DataManager.Instance.SaveDataGame();
    }
    public void GameFileLoad()
    {
        DataManager.Instance.LoadGameData();
    }

    public void GameFileSave()
    {
        DataManager.Instance.SaveDataGame();
    }

}
