using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Animator monster;
    public Animator player;
    public GameObject hpBar;

    private int hp = 0;
    private int hpMax = 0;

    // Start is called before the first frame update
    void Start()
    {
        hpMax = 10;
        hp = hpMax;
        monster.SetInteger("hp", hp);
    }

    // Update is called once per frame
    void Update()
    {
        hp = monster.GetInteger("hp");
        print("hp:" + hp);
        if (monster.GetBool("dead"))
        {
            StartCoroutine(DelayThenFall(2.0f));
        }
        if (hp <= 0)
        {
            monster.SetInteger("hp", 0);
            monster.SetBool("dead", true);
        }
        if (hp >= 0)
        {
            float _percent = ((float)hp / (float)hpMax);
            hpBar.transform.localScale = new Vector3(_percent, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        }
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

    IEnumerator DelayThenFall(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
