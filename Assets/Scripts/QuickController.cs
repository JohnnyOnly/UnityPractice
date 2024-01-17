using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickController : MonoBehaviour
{
    public GameObject playerManager;

    void Start() {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager");
    }

    public void walkUp() {
        print("quick up");
        playerManager.GetComponent<PlayerManager>().walkUp();
    }

    public void walkDown() {
        print("quick down");
        playerManager.GetComponent<PlayerManager>().walkDown();
    }

    public void walkLeft() {
        print("quick left");
        playerManager.GetComponent<PlayerManager>().walkLeft();
    }

    public void walkRight() {
        print("quick right");
        playerManager.GetComponent<PlayerManager>().walkRight();
    }
}
