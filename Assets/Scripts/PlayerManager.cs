using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerObject;

    private Animator playerAnim;
    private float speed = 0.1f;
    private bool isWalking = false;
    private bool isAttacking = false;
    private Vector2 worldPosLeftBottom;
    private Vector2 worldPosTopRight;
    private float timeSpeed;
    private PolygonCollider2D collider2D;

    void Start()
    {
        print("PlayerManager Start");
        playerAnim = playerObject.GetComponent<Animator>();
        collider2D = playerObject.GetComponentInChildren<PolygonCollider2D>();

        worldPosLeftBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
        worldPosTopRight = Camera.main.ViewportToWorldPoint(Vector2.one);

    } 

    // Update is called once per frame
    void Update()
    {
        timeSpeed = speed * Time.deltaTime * 60;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.walkUp();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.walkDown();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.walkLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.walkRight();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            this.attack();
        }
        else
        {
            if (!isWalking && !isAttacking)
            {
                this.disableWalk();
            }
        }

        AnimatorClipInfo[] clip = playerAnim.GetCurrentAnimatorClipInfo(0);
        string animName = "";
        if (clip.Length > 0)
        {
            animName = clip[0].clip.name;
        }

        if (isWalking || animName.Equals("player-attack"))
        {
            playerAnim.enabled = true;
            isWalking = false;
        }
        else
        {
            playerAnim.enabled = false;
        }
 
        Vector3 currentPosition = playerObject.transform.position;
        //避免超出場景
        playerObject.transform.position = new Vector3(Mathf.Clamp(currentPosition.x, worldPosLeftBottom.x, worldPosTopRight.x), Mathf.Clamp(currentPosition.y, worldPosLeftBottom.y, worldPosTopRight.y), 0.0f);
        
    }

    public void walkUp() {
        print("up");
        playerAnim.Play("walk-up");
        playerObject.transform.position += new Vector3(0, timeSpeed, 0);
        isWalking = true;
    }

    public void walkDown() {
        print("down");
        playerAnim.Play("walk-down");
        playerObject.transform.position -= new Vector3(0, timeSpeed, 0);
        isWalking = true;
    }

    public void walkLeft() {
        print("left");
        playerAnim.Play("walk-left");
        playerObject.transform.position -= new Vector3(timeSpeed, 0, 0);
        isWalking = true;
    }

    public void walkRight() {
        print("right");
        playerAnim.Play("walk-right");
        playerObject.transform.position += new Vector3(timeSpeed, 0, 0);
        isWalking = true;
    }

    public void disableWalk() {
        isWalking = false;
    }

    public void attack()
    {
        print("attack");
        playerAnim.enabled = true;
        collider2D.enabled = true;
        playerAnim.SetTrigger("attack");
        StartCoroutine(DisableHixBox());
        isWalking = true;
        isAttacking = true;
    }

    IEnumerator DisableHixBox()
    {
        yield return new WaitForSeconds(0.5f);
        collider2D.enabled = false;
        isAttacking = false;
    }

}
