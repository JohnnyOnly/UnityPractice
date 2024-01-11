using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMove : MoveStrategy
{
    private GameObject playerObject;

    public FollowMove(GameObject playerObject)
    {
        this.playerObject = playerObject;
    }

    public override Vector2 moveStrategy()
    {
        return playerObject.transform.position;
    }

}
