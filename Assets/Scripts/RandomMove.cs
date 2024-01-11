using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MoveStrategy
{
    private Vector2 leftDownPos;
    private Vector2 rightUpPos;

    public RandomMove(Vector2 leftDownPos, Vector2 rightUpPos)
    {
        this.leftDownPos = leftDownPos;
        this.rightUpPos = rightUpPos;
    }

    public override Vector2 moveStrategy()
    {
        Vector2 randomPos = new Vector2(Random.Range(leftDownPos.x, rightUpPos.x), Random.Range(leftDownPos.y, rightUpPos.y));
        return randomPos;
    }

}
