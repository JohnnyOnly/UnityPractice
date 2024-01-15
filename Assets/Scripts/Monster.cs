using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public GameObject hpBar;

    protected Animator monster;
    protected Animator player;
    protected int hpMax = 0;
    protected int hp = 0;
    
    private Vector2 movePos;
    private MoveStrategy moveStrategy;

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        print("hp:" + hp);
        float _percent = ((float)hp / (float)hpMax);
        hpBar.transform.localScale = new Vector3(_percent, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        if (hp <= 0)
        {
            monster.SetTrigger("dead");
            Invoke("Dead", 1.0f);
        }

    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        monster.SetTrigger("hurt");
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        print("進入碰撞: " + coll.gameObject.name);
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        print("離開碰撞: " + coll.gameObject.name);
    }

    // StartCoroutine(DelayThenFall(1.5f));
    IEnumerator DelayThenFall(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }

    void Dead()
    {
        Destroy(this.gameObject);
    }

    public void SetMoveStrategy(MoveStrategy moveStrategy)
    {
        this.moveStrategy = moveStrategy;
    }

    public MoveStrategy GetMoveStrategy()
    {
        return moveStrategy;
    }

    public void SetMovePos(Vector2 movePos)
    {
        this.movePos = movePos;
    }

    public Vector2 GetMovePos()
    {
        return movePos;
    }

    public Vector2 PerformMove()
    {
        return moveStrategy.moveStrategy();
    }
}
