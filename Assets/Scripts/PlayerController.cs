using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int playerHp = 0;
    private int playerHpMax = 100;

    private GameObject playerManager;

    public Image hpBar;


    // Start is called before the first frame update
    void Start()
    {
        print("Start");
        playerHp = playerHpMax;

        playerManager = GameObject.FindGameObjectWithTag("PlayerManager");
    }

    // Update is called once per frame
    void Update()
    {
        //上下左右, 1234
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.MoveUp();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.MoveDown();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.MoveRight();
        }
        else
        {
            this.DisableWalk();
        }

        float _percent = ((float)playerHp / (float)playerHpMax);
        hpBar.transform.localScale = new Vector3(_percent, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        print("currentHp:" + playerHp);
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        print("進入碰撞: " + coll.gameObject.name);
        playerHp--;
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        print("離開碰撞: " + coll.gameObject.name);
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        print("持續碰撞: " + coll.gameObject.name);
        //monsterAnim = coll.gameObject.GetComponent<Animator>();
    }


    private void MoveUp() {
        print("move up");
        playerManager.GetComponent<PlayerManager>().walkUp();
    }

    private void MoveDown() {
        print("move down");
        playerManager.GetComponent<PlayerManager>().walkDown();
    }

    private void MoveLeft() {
        print("move left");
        playerManager.GetComponent<PlayerManager>().walkLeft();
    }

    private void MoveRight() {
        print("move right");
        playerManager.GetComponent<PlayerManager>().walkRight();
    }

    private void DisableWalk(){
        playerManager.GetComponent<PlayerManager>().disableWalk();
    }
}