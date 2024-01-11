using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private int damage = 1;
    private Animator player;
    private PolygonCollider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        //attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Attack");
            player.enabled = true;
            collider2D.enabled = true;
            player.SetTrigger("attack");
            StartCoroutine(DisableHixBox());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("OnTriggerEnter2D:" + other.gameObject.name);
        if (other.gameObject.CompareTag("Monster"))
        {
            other.GetComponent<Monster>().TakeDamage(damage);
        }
    }

    IEnumerator DisableHixBox()
    {
        yield return new WaitForSeconds(0.5f);
        collider2D.enabled = false;
    }
}
