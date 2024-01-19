using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkController : Monster
{
    public float speed;
    public float startWaitTime;

    private float waitTime;

    private float a = 0;
    private float z = 0;
    private bool forward = false;

    private Vector2 movePos;

    // Start is called before the first frame update
    public void Start()
    {
        hpMax = 5;
        hp = hpMax;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        monster = GameObject.FindGameObjectWithTag("Monster").GetComponent<Animator>();

        waitTime = startWaitTime;

    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();

        if(GetMoveStrategy() == null)
        {
            return;
        }

        movePos = GetMovePos();
        //print("movePos:" + movePos);
        transform.position = Vector2.MoveTowards(transform.position, movePos, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, movePos) < 0.1f)
        {
            if(waitTime <= 0)
            {
                SetMovePos(PerformMove());
                waitTime = startWaitTime; 
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

    }

    Vector3 GetSWalkPos()
    {
        a += 0.01f;//弧度每帧增加0.01f
        Vector3 v3x = this.transform.right * Time.deltaTime * 1.0f;
        Vector3 v3y = this.transform.up * Mathf.Sin(a) * 0.05f;
        //this.transform.Translate(this.transform.right * Time.deltaTime * 1.0f);//物体朝着自己的x轴正方向移动，移动速度为speed*deltatime
        //this.transform.Translate(this.transform.up * Mathf.Sin(a) * 0.05f);//物体y轴方向移动是sin曲线形式（mathf.sin(a)表示将a这个弧度传入并转化为对应数值）（后面*0.1f是为了减弱速度）
        Vector2 v2x = Vector2.right * Time.deltaTime * 1.0f;
        Vector2 v2y = Vector2.up * Mathf.Sin(a) * 0.05f;
        
        return v3y;
    }

    Vector2 GetSWalkPos2()
    {
        if(z * Mathf.Deg2Rad >= 2 * 3.14)
        {
            forward = false;
        }
        else if(z * Mathf.Deg2Rad < 0)
        {
            forward = true;
        }
        if (forward)
        {
            z += 1;
        }
        else
        {
            z -= 1;
        }
        float x = Mathf.Sin(z * Mathf.Deg2Rad);
        Vector2 sWalkPos = new Vector2(x * 3, z * 0.1f);
        return sWalkPos;

        /*
        a += 0.01f;//弧度每帧增加0.01f
        this.transform.Translate(Vector2.right * Time.deltaTime * 1.0f);//物体朝着自己的x轴正方向移动，移动速度为speed*deltatime
        this.transform.Translate(Vector2.up * Mathf.Sin(a) * 0.05f);//物体y轴方向移动是sin曲线形式（mathf.sin(a)表示将a这个弧度传入并转化为对应数值）（后面*0.1f是为了减弱速度）
        */
    }

}
