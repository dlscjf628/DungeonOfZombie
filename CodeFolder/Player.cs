using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, GunDamage
{
    float speed = 5f;
    Rigidbody2D rigid;
    Animator ani;
    GameObject unit;
    GameObject Ak47;
    public GameObject HE;//수류탄
    public GameObject heLocation; //수류탄 나가는 위치
    public GameObject map;
    public GameObject recovery;
    public GameObject permanent;
    public GameObject Inventory;
    public Image hpBar;
    public Image hpIcon;
    public Text hpText;
    public Item HEBullet;
    public GameObject music;
    public bool UIOpen = true;
    public Item[] items;
    public GameObject Mouse { get; set; }
    Weapon ak;
    Gun gun;
    ShotGun shotGun;
    Sniper sniper;
    Grenade grenade;
    Recovery potion;
    Permanent perPotion;
    Canvas can;
    Canvas Ui;
    public float hpMax = 10000f;
    public float hp = 10000f;
    float t = 3f;
    bool b = false;
    int cnt;
    bool m;
    public bool Laberis;
    public bool slow, poison;
    public bool D1 = false, D2 = false, D3 = false, D4 = false;
    float t2 = 0f;//수류탄 딜레이
    public GameObject panel;
    Text text;
    Vector2 mouse;
    PlayerInput playInput;
    PlayerWeapon playerWeapon;


    // Start is called before the first frame update
    void Start()
    {
        unit = transform.GetChild(0).gameObject;
        Ak47 = transform.GetChild(1).gameObject;
        rigid = GetComponent<Rigidbody2D>();
        ani = unit.GetComponent<Animator>();
        playInput = GetComponent<PlayerInput>();
        playerWeapon = GetComponent<PlayerWeapon>();
        ak = Ak47.GetComponent<Weapon>();
        shotGun = transform.GetChild(3).gameObject.GetComponent<ShotGun>();
        gun = transform.GetChild(4).gameObject.GetComponent<Gun>();
        sniper= transform.GetChild(5).gameObject.GetComponent<Sniper>();
        grenade = HE.GetComponent<Grenade>();
        potion = recovery.GetComponent<Recovery>();
        perPotion = permanent.GetComponent<Permanent>();
        Mouse = GameObject.Find("Mouse");
        can = GameObject.Find("Canvas").gameObject.GetComponent<Canvas>();
        Ui = GameObject.Find("UI").gameObject.GetComponent<Canvas>();
        panel = can.transform.GetChild(6).gameObject;
        text = panel.transform.GetChild(2).gameObject.GetComponent<Text>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!(Manager.instance != null && Manager.instance.gameOver))
        {
            Move();
            Angle();
            PlayerInput();
            Roll2();
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Inventory.activeSelf == true)
            {
                Inventory.SetActive(false);
                Mouse.SetActive(true);
                UIOpen = true;
            }
            else if (Inventory.activeSelf == false)
            {
                Inventory.SetActive(true);
                Mouse.SetActive(false);
                UIOpen = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.G) && Laberis) // 연구소장 상호작용
        {
            if (panel.activeSelf == true)
            {
                panel.SetActive(false);
                Mouse.SetActive(true);
                UIOpen = true;
            }
            else if (panel.activeSelf == false)
            {
                Mouse.SetActive(false);
                UIOpen = false;
                panel.SetActive(true);
                if (D1 && !D2 && !D3 && !D4 && items[0].num == 1)
                {
                    StartCoroutine(LaberTalk1());
                }
                else if (D1 && !D2 && !D3 && !D4 && items[0].num == 0)
                {
                    StartCoroutine(LaberTalk1_1());
                }
                else if (D1 && D2 && !D3 && !D4 && items[1].num == 1)
                {
                    StartCoroutine(LaberTalk2());
                }
                else if (D1 && D2 && !D3 && !D4 && items[1].num == 0)
                {
                    StartCoroutine(LaberTalk2_1());
                }
                else if (D1 && D2 && D3 && !D4 && items[2].num == 1)
                {
                    StartCoroutine(LaberTalk3());
                }
                else if (D1 && D2 && D3 && !D4 && items[2].num == 0)
                {
                    StartCoroutine(LaberTalk3_1());
                }
                else if (D1 && D2 && D3 && D4 && items[3].num == 1)
                {
                    StartCoroutine(LaberTalk4());
                }
                else if (D1 && D2 && D3 && D4 && items[3].num == 0)
                {
                    StartCoroutine(LaberTalk4_1());
                }
            }
        }
        if (slow)
        {
            slow = false;
            StartCoroutine(Slow());
        }
        if (poison)
        {
            poison = false;
            StartCoroutine(Poison(5));
        }
        hpBarText();
    }

    //캐릭터 움직임
    void Move()
    {
        t += Time.deltaTime;
        t2 += Time.deltaTime;
        Vector2 move = new Vector2(playInput.move1, playInput.move2) * speed;
        rigid.velocity = move;
       

        if (!(rigid.velocity.x == 0 && rigid.velocity.y == 0) && b == false)
        {
            ani.SetBool("Run", true);
        }
        else
        {
            ani.SetBool("Run", false);
        }
      
    }

    void Angle()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (direction.x > transform.position.x)
            unit.transform.localScale = new Vector3(-1, 1, 1);
        if (direction.x <= transform.position.x)
            unit.transform.localScale = new Vector3(1, 1, 1);
    }

    void PlayerInput()
    {
        if (UIOpen)
        {
            if (playInput.shot)  //좌클릭을 해 공격했을 경우
            {
                if (playerWeapon.weaponeNum == 1)
                {
                    ak.Shot();
                    shotGun.Shot();
                    gun.Shot();
                    sniper.Shot();
                }
                else if (playerWeapon.weaponeNum == 2)
                {
                    if (t2 > 3f && playerWeapon.recoveryCnt > 0)
                    {
                        float t = hp + potion.recovery;
                        if (t > hpMax)
                            t = hpMax;
                        hp = t;
                        playerWeapon.recoveryCnt--;
                        StartCoroutine(WeaponeSwitching());
                        print(hp);
                        t2 = 0;
                    }
                }
                else if (playerWeapon.weaponeNum == 3)
                {
                    if (t2 > 3f && playerWeapon.permanentCnt > 0)
                    {
                        hpMax += perPotion.health;
                        print(hpMax);
                        playerWeapon.permanentCnt--;
                        StartCoroutine(WeaponeSwitching());
                        t2 = 0;
                    }
                }
                else if (playerWeapon.weaponeNum == 4)
                {
                    if (t2 > 3f && playerWeapon.grenadeCnt > 0)
                    {
                        Instantiate(HE, heLocation.transform.position, Quaternion.identity);
                        playerWeapon.grenadeCnt--;
                        StartCoroutine(WeaponeSwitching());
                        t2 = 0;
                        HEBullet.num--;
                    }
                }
            }
            else if (playInput.reload) //r키를 눌러 장전 했을 경운
            {
                if (playerWeapon.weaponeNum == 1)
                {
                    if (playerWeapon.weaponCnt == 0)
                    {
                        gun.Reload();
                    }
                    else if (playerWeapon.weaponCnt == 1)
                    {
                        ak.Reload();
                    }
                    else if (playerWeapon.weaponCnt == 2)
                    {
                        shotGun.Reload();
                    }
                    else if (playerWeapon.weaponCnt == 3)
                    {
                        sniper.Reload();
                    }
                }
                else if (playerWeapon.weaponeNum == 2)
                {

                }
                else if (playerWeapon.weaponeNum == 3)
                {

                }
                else if (playerWeapon.weaponeNum == 4)
                {

                }
            }
            else if (playInput.Space && t > 3f)
            {
                Roll();
            }
            else if (playInput.tap)
            {
                if (!m)
                {
                    map.SetActive(true);
                    m = true;
                }
                else
                {
                    map.SetActive(false);
                    m = false;
                }
            }
            else //총을 쏘지 않으면 불 이펙트가 꺼짐
            {
                ak.mouseUI.SetActive(false);
            }
        }
        
    }

    //구르기 이펙트
    void Roll()
    {
        t = 0;
        b = true;

        if (unit.transform.localScale.x > 0)
        {
            ani.SetBool("Roll", true);
            StartCoroutine(stop());
        }
        else if (unit.transform.localScale.x == -1)
        {
            ani.SetBool("Roll2", true);
            StartCoroutine(stop2());
        }

    }

    //멈춰있을때 구르기를 할때
    void Roll2()
    {
        if (b)
        {

            speed = 10f;

            if (rigid.velocity == Vector2.zero)
            {
                if (cnt == 0)
                {
                    mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    cnt++;
                }
                rigid.velocity = mouse.normalized * speed;
            }

            StartCoroutine(stop3());
        }
    }

    public virtual void OnDamage(float damage)
    {
        hp -= damage;
        print(hp);
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Manager.instance.gameOver = true;
        ani.SetTrigger("Die");
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        StartCoroutine(playerDie());
    }

    void hpBarText()
    {
        hpBar.fillAmount = hp / hpMax;
        hpText.text = hpMax.ToString() + "/" + hp.ToString();
    }

    IEnumerator stop()
    {

        yield return new WaitForSeconds(0.4f);
        ani.SetBool("Roll", false);
        
    }

    IEnumerator stop2()
    {

        yield return new WaitForSeconds(0.4f);
        ani.SetBool("Roll2", false);

    }
    IEnumerator LaberTalk1()
    {
        text.text = "안녕? 난 연구소장이다. 튜토리얼 던전을 클리어 했구나!";
        yield return new WaitForSeconds(1.5f);
        text.text = "너는 모든 던전의 보스를 처치해 보스 좀비의 진피를 모아야한다.";
        yield return new WaitForSeconds(1.5f);
        text.text = "진피는 총 4개다. 4개의 진피를 모으면 백신을 만들 수 있단다.";
        yield return new WaitForSeconds(1.5f);
        text.text = "백신을 만들면 좀비들을 치료할 수 있단다.";
        yield return new WaitForSeconds(1.5f);
        text.text = "모든 던전을 클리어하고 보스 좀비의 진피를 모아오거라!";
    }
    IEnumerator LaberTalk1_1()
    {
        text.text = "안녕? 난 연구소장이다. 튜토리얼 던전을 클리어 했구나!";
        yield return new WaitForSeconds(1.5f);
        text.text = "너는 모든 던전의 보스를 처치해 보스 좀비의 진피를 모아야한다.";
        yield return new WaitForSeconds(1.5f);
        text.text = "진피는 총 4개다. 4개의 진피를 모으면 백신을 만들 수 있단다.";
        yield return new WaitForSeconds(1.5f);
        text.text = "백신을 만들면 좀비들을 치료할 수 있단다.";
        yield return new WaitForSeconds(1.5f);
        text.text = "허나 너는 진피를 들고오지 않았구나.";
        yield return new WaitForSeconds(1.5f);
        text.text = "이번만 내가 줄테니 다음부터는 꼭 들고오거라.";
        yield return new WaitForSeconds(1.5f);
        items[0].num++;
        text.text = "남은 진피는 3개다. 다음 보스를 처치하고 보스 좀비의 진피를 가져오거라!";
    }
    IEnumerator LaberTalk2()
    {
        text.text = "두번째 던전을 클리어 했구나!";
        yield return new WaitForSeconds(1.5f);
        text.text = "남은 진피는 2개다. 다음 보스를 처치하고 보스 좀비의 진피를 가져오거라!";
    }
    IEnumerator LaberTalk2_1()
    {
        text.text = "두번째 던전을 클리어 했구나!";
        yield return new WaitForSeconds(1.5f);
        text.text = "진피를 가져오지 않았구나. 다시 가서 보스를 처치하고 가져오너라.";
        yield return new WaitForSeconds(1.5f);
        D2 = false;
        SceneManager.LoadScene("Dungeon1");
    }
    IEnumerator LaberTalk3()
    {
        text.text = "세번째 던전을 클리어 했구나!";
        yield return new WaitForSeconds(1.5f);
        text.text = "남은 진피는 1개다. 다음 보스를 처치하고 보스 좀비의 진피를 가져오거라!";
    }
    IEnumerator LaberTalk3_1()
    {
        text.text = "세번째 던전을 클리어 했구나!";
        yield return new WaitForSeconds(1.5f);
        text.text = "진피를 가져오지 않았구나. 다시 가서 보스를 처치하고 가져오너라.";
        yield return new WaitForSeconds(1.5f);
        D3 = false;
        SceneManager.LoadScene("Dungeon2");
    }
    IEnumerator LaberTalk4()
    {
        text.text = "마지막 던전을 클리어 했구나!";
        yield return new WaitForSeconds(1.5f);
        text.text = "백신을 만들 수 있겠어!";
        yield return new WaitForSeconds(1.5f);
        text.text = "조금만 기다려라! 백신을 만들어 올테니!";
        yield return new WaitForSeconds(1.5f);
        text.text = "백신이 완성되었다! 이걸로 모든 좀비를 사람으로 치료 할 수 있겠어!";
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Clear());
    }
    IEnumerator LaberTalk4_1()
    {
        text.text = "마지막 던전을 클리어 했구나!";
        yield return new WaitForSeconds(1.5f);
        text.text = "진피를 가져오지 않았구나. 다시 가서 보스를 처치하고 가져오너라.";
        yield return new WaitForSeconds(1.5f);
        D4 = false;
        SceneManager.LoadScene("Dungeon3");
    }
    IEnumerator stop3()
    {
        yield return new WaitForSeconds(0.5f);
        cnt = 0;
        speed = 5f;
        b = false;
    }
    IEnumerator WeaponeSwitching()
    {
        yield return new WaitForSeconds(0.5f);
        playInput.change = 1;
    }
    IEnumerator playerDie()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject, 0.1f);
        Destroy(hpBar.gameObject, 0.1f);
        Destroy(hpIcon.gameObject, 0.1f);
        Destroy(hpText.gameObject, 0.1f);
        Destroy(can.gameObject, 0.1f);
        Destroy(Ui.gameObject, 0.1f);
        Destroy(music, 0.1f);
        Manager.instance.PlayerDie();
    }
    IEnumerator Clear()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject, 0.1f);
        Destroy(hpBar.gameObject, 0.1f);
        Destroy(hpIcon.gameObject, 0.1f);
        Destroy(hpText.gameObject, 0.1f);
        Destroy(can.gameObject, 0.1f);
        Destroy(Ui.gameObject, 0.1f);
        Manager.instance.Ending();
    }
    IEnumerator Return()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject, 0.1f);
        Destroy(hpBar.gameObject, 0.1f);
        Destroy(hpIcon.gameObject, 0.1f);
        Destroy(hpText.gameObject, 0.1f);
        Destroy(can.gameObject, 0.1f);
        Destroy(Ui.gameObject, 0.1f);
        Manager.instance.Return();
    }
    IEnumerator Slow()
    {
        speed = 3f;
        yield return new WaitForSeconds(3f);
        speed = 5f;
    }
    IEnumerator Poison(int a)
    {
        hpIcon.color = new Color(0, 1, 0, 1);
        hpBar.color = new Color(0, 1, 0, 1);
        hp -= 1f;
        yield return new WaitForSeconds(1f);
        if (a != 0)
        {
            StartCoroutine(Poison(--a));
        }
        else
        {
            hpIcon.color = new Color(1, 1, 1, 1);
            hpBar.color = new Color(1, 1, 1, 1);
        }
    }
}
