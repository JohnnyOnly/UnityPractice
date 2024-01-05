using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Animator monster;
    public Animator player;
    public GameObject hpBar;

    private int hp = 0;
    private readonly int hpMax = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        monster = GameObject.FindGameObjectWithTag("Monster").GetComponent<Animator>();
        hp = hpMax;
    }

    // Update is called once per frame
    void Update()
    {
        print("hp:" + hp);
        float _percent = ((float)hp / (float)hpMax);
        hpBar.transform.localScale = new Vector3(_percent, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        if(hp<=0)
        {
            monster.SetTrigger("dead");
            Invoke("Dead", 1.2f);
            //StartCoroutine(DelayThenFall(1.0f));
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
        player = coll.gameObject.GetComponent<Animator>();
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        print("離開碰撞: " + coll.gameObject.name);
        player = null;
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
}
