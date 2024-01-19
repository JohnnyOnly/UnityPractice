using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private int damage = 1;
    private GameObject playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space)){
            playerManager.GetComponent<PlayerManager>().attack();
        }
        */
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("OnTriggerEnter2D:" + other.gameObject.name);
        if (other.gameObject.CompareTag("Monster"))
        {
            other.GetComponent<Monster>().TakeDamage(damage);
        }
    }

}
