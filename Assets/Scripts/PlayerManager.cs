using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerObject;

    private Animator playerAnim;
    private float speed = 0.05f;
    private bool isWalking = false;
    private Vector2 worldPosLeftBottom;
    private Vector2 worldPosTopRight;
    private float timeSpeed;
    private Vector3 currentPosition;

    void Start()
    {
        print("PlayerManager Start");
        playerAnim = playerObject.GetComponent<Animator>();
        
        worldPosLeftBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
        worldPosTopRight = Camera.main.ViewportToWorldPoint(Vector2.one);

        currentPosition = playerObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //currentPosition = this.gameObject.transform.position;

        timeSpeed = speed * Time.deltaTime * 60;

        if (isWalking)
        {
            playerAnim.enabled = true;
        }
        else
        {
            AnimatorClipInfo[] clip = playerAnim.GetCurrentAnimatorClipInfo(0);
            string animName = "";
            if (clip.Length > 0)
            {
                animName = clip[0].clip.name;
            }
            print("current animName:" + animName);
            if (animName.Equals("player-attack"))
            {
                playerAnim.enabled = true;
            }
            else
            {
                playerAnim.enabled = false;
            }
        }
 
        //避免超出場景
        playerObject.transform.position = new Vector3(Mathf.Clamp(currentPosition.x, worldPosLeftBottom.x, worldPosTopRight.x), Mathf.Clamp(currentPosition.y, worldPosLeftBottom.y, worldPosTopRight.y), 0.0f);
    }

    public void walkUp() {
        print("up");
        playerAnim.Play("walk-up");
        currentPosition += new Vector3(0, timeSpeed, 0);
        isWalking = true;
    }

    public void walkDown() {
        print("down");
        playerAnim.Play("walk-down");
        currentPosition -= new Vector3(0, timeSpeed, 0);
        isWalking = true;
    }

    public void walkLeft() {
        print("left");
        playerAnim.Play("walk-left");
        currentPosition -= new Vector3(timeSpeed, 0, 0);
        isWalking = true;
    }

    public void walkRight() {
        print("right");
        playerAnim.Play("walk-right");
        currentPosition += new Vector3(timeSpeed, 0, 0);
        isWalking = true;
    }

    public void disableWalk() {
        isWalking = false;
    }
}
