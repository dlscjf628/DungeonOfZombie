using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float move1 { get; set; }  //좌우 이동
    public float move2 { get; set; }  // 상하 이동
    public bool shot { get; set; }  //총알 발사
    public bool reload { get; set; } //총알 장전
    public bool UI { get; set; } //아이템 UI
    public bool Space { get; set; } //구르기
    public bool tap { get; set; } //Tap키
    public float change { get; set; } //무긴 스위칭

    KeyCode[] number = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };
    // Start is called before the first frame update
    void Start()
    {
        change = 1;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.instance != null && Manager.instance.gameOver)  //게임 오버시 플레이어 멈춤
        {
            move1 = 0;
            move2 = 0;
            shot = false;
            reload = false;
            return;
        }
        //change = 0;
        move1 = Input.GetAxis("Horizontal");
        move2 = Input.GetAxis("Vertical");
        shot = Input.GetMouseButton(0);
        reload = Input.GetKey(KeyCode.R);
        UI = Input.GetKey(KeyCode.I);
        Space = Input.GetKey(KeyCode.Space);
        tap = Input.GetKeyDown(KeyCode.Tab);

        if (Input.GetKeyDown(number[0])) change = 1;
        else if (Input.GetKeyDown(number[1])) change = 2;
        else if (Input.GetKeyDown(number[2])) change = 3;
        else if (Input.GetKeyDown(number[3])) change = 4;
        else if (Input.GetKeyDown(number[4])) change = 5;

    }
}
