using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickController : MonoBehaviour
{
    private GameObject playerManager;
    private GameObject player;

    void Start() {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void walkUp() {
        print("quick up");
        playerManager.GetComponent<PlayerManager>().walkUp();
    }

    public void walkDown() {
        print("quick down");
        for(int i=0; i<10; i++) {
            playerManager.GetComponent<PlayerManager>().walkDown();
        }
    }

    public void walkLeft() {
        print("quick left");
        playerManager.GetComponent<PlayerManager>().walkLeft();
    }

    public void walkRight() {
        print("quick right");
        playerManager.GetComponent<PlayerManager>().walkRight();
    }

    public void attack() {
        print("quick attack");
        playerManager.GetComponent<PlayerManager>().attack();
        /*
        PlayerManager manager = playerManager.GetComponent<PlayerManager>();
        Transform[] transforms = manager.playerObject.GetComponentsInChildren<Transform>();
        for(int i=0; i<transforms.Length; i++){
            print(transforms[i].name);
        }
        manager.attack();
        */
    }

    public void addHp() {
        player.GetComponent<PlayerController>().addHp();
    }
}
