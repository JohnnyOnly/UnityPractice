using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public Animator player;

    private float speed = 0.05f;
    private int status = 0;
    private int face = 0;
    private int attack = -1;
    private int hurt = 0;
    private bool collision = false;
    private bool hurtEnable = false;

    private Vector2 worldPosLeftBottom;
    private Vector2 worldPosTopRight;
    private Animator monsterAnim;

    private int playerHp = 0;
    private int playerHpMax = 100;

    public Image hpBar;

    // Start is called before the first frame update
    void Start()
    {
        print("Start");
        worldPosLeftBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
        worldPosTopRight = Camera.main.ViewportToWorldPoint(Vector2.one);

        playerHp = playerHpMax;
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = false;
        hurtEnable = false;

        Vector3 currentPosition = this.gameObject.transform.position;
        //上下左右, 1234
        float timeSpeed = speed * Time.deltaTime * 60;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            print("Up");
            isWalking = true;
            status = 1;
            currentPosition += new Vector3(0, timeSpeed, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            print("Down");
            isWalking = true;
            status = 2;
            currentPosition -= new Vector3(0, timeSpeed, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            print("Left");
            isWalking = true;
            status = 3;
            currentPosition -= new Vector3(timeSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            print("Right");
            isWalking = true;
            status = 4;
            currentPosition += new Vector3(timeSpeed, 0, 0);
        }
        //避免超出場景
        this.gameObject.transform.position = new Vector3(Mathf.Clamp(currentPosition.x, worldPosLeftBottom.x, worldPosTopRight.x), Mathf.Clamp(currentPosition.y, worldPosLeftBottom.y, worldPosTopRight.y), 0.0f);

        if (isWalking)
        {
            player.SetInteger("status", status);
        }
        else
        {
            player.SetInteger("status", 0);
            player.SetInteger("face", status);
            player.SetInteger("attack", 0);
        }

        //attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Attack");
            attack = 1;
            hurt = 1;
            hurtEnable = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            print("Attack off");
            attack = 0;
            hurt = 0;
        }

        if (collision)
        {
            player.SetInteger("attack", attack);
            monsterAnim.SetInteger("hurt", hurt);
            hurt = monsterAnim.GetInteger("hurt");
            print("hurt:" + hurt);
            int hp = monsterAnim.GetInteger("hp");
            if (hurt == 1 && hurtEnable)
            {
                hp--;
                monsterAnim.SetInteger("hp", hp);
                hurtEnable = false;
            }
        }

        float _percent = ((float)playerHp / (float)playerHpMax);
        hpBar.transform.localScale = new Vector3(_percent, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        print("currentHp:" + playerHp);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        print("進入碰撞: " + coll.gameObject.name);
        collision = true;
        monsterAnim = coll.gameObject.GetComponent<Animator>();
        playerHp--;
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        print("離開碰撞: " + coll.gameObject.name);
        monsterAnim = null;
        collision = false;
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        print("持續碰撞: " + coll.gameObject.name);
        monsterAnim = coll.gameObject.GetComponent<Animator>();
        collision = true;
    }
}