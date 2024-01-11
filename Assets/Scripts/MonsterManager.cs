using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private Vector2 worldPosLeftBottom;
    private Vector2 worldPosTopRight;

    public GameObject monsterPrefab;

    private GameObject monsterObject;
    private GameObject playerObject;

    private int count = 0;
    private MoveStrategy moveStrategy;

    // Start is called before the first frame update
    void Start()
    {
        worldPosLeftBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
        worldPosTopRight = Camera.main.ViewportToWorldPoint(Vector2.one);
        playerObject = GameObject.FindGameObjectWithTag("Player");

        init();
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterObject == null)
        {
            count++;
            monsterObject = CreateMonster();
            Monster monster = monsterObject.GetComponent<Monster>();
            if (count % 2 == 0)
            {
                moveStrategy = new RandomMove(worldPosLeftBottom, worldPosTopRight);
            }
            else
            {
                moveStrategy = new FollowMove(playerObject);
            }
            monster.SetMoveStrategy(moveStrategy);
            monster.SetMovePos(moveStrategy.moveStrategy());
        }
    }

    void init()
    {
        monsterObject = CreateMonster();
        Monster monster = monsterObject.GetComponent<Monster>();
        MoveStrategy moveStrategy = new RandomMove(worldPosLeftBottom, worldPosTopRight);
        monster.SetMoveStrategy(moveStrategy);
        monster.SetMovePos(moveStrategy.moveStrategy());
    }

    Vector2 GetRandomShowPos()
    {
        Vector2 randomPos = new Vector2(Random.Range(worldPosLeftBottom.x, worldPosTopRight.x), Random.Range(worldPosLeftBottom.y, worldPosTopRight.y));
        return randomPos;
    }

    GameObject CreateMonster()
    {
        return Instantiate(monsterPrefab, GetRandomShowPos(), Quaternion.identity);
    }

}
