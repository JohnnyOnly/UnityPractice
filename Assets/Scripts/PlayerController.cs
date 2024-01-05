using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public Animator player; //Animation anim, Animator player

    private float speed = 0.05f;
    private bool isWalking = false;

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

        player = GetComponent<Animator>();
        playerHp = playerHpMax;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 currentPosition = this.gameObject.transform.position;
        //上下左右, 1234
        float timeSpeed = speed * Time.deltaTime * 60;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            print("Up");
            player.Play("walk-up");
            currentPosition += new Vector3(0, timeSpeed, 0);
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            print("Down");
            player.Play("walk-down");
            currentPosition -= new Vector3(0, timeSpeed, 0);
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            print("Left");
            player.Play("walk-left");
            currentPosition -= new Vector3(timeSpeed, 0, 0);
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            print("Right");
            player.Play("walk-right");
            currentPosition += new Vector3(timeSpeed, 0, 0);
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if (isWalking)
        {
            player.enabled = true;
        }
 
        //避免超出場景
        this.gameObject.transform.position = new Vector3(Mathf.Clamp(currentPosition.x, worldPosLeftBottom.x, worldPosTopRight.x), Mathf.Clamp(currentPosition.y, worldPosLeftBottom.y, worldPosTopRight.y), 0.0f);

        /*
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
        */

        float _percent = ((float)playerHp / (float)playerHpMax);
        hpBar.transform.localScale = new Vector3(_percent, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        print("currentHp:" + playerHp);
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        print("進入碰撞: " + coll.gameObject.name);
        monsterAnim = coll.gameObject.GetComponent<Animator>();
        playerHp--;
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        print("離開碰撞: " + coll.gameObject.name);
        monsterAnim = null;
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        print("持續碰撞: " + coll.gameObject.name);
        monsterAnim = coll.gameObject.GetComponent<Animator>();
    }
}