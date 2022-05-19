using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction //방향 열거문
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public struct PosDir
{
    public Vector2 pos;
    public Direction dir;
}


public class HeroMoveController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anit;
    public float speed = 6;

    public Direction dir = Direction.UP;
    public bool isHead = false;
    public bool isTurn = false;

    public int idx = 0;

    public List<PosDir> prePos = new List<PosDir>();

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anit = GetComponentInChildren<Animator>();
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if (prePos.Count != 0)
        {
            isTurn = true;

            if (dir == Direction.UP)
            {
                if (transform.position.y >= prePos[0].pos.y)
                {
                    transform.position = prePos[0].pos;
                    SetAnim(prePos[0].dir);
                    Move();

                    prePos.RemoveAt(0);
                }
            }
            else if (dir == Direction.DOWN)
            {
                if (transform.position.y <= prePos[0].pos.y)
                {
                    transform.position = prePos[0].pos;
                    SetAnim(prePos[0].dir);
                    Move();

                    prePos.RemoveAt(0);
                }
            }
            else if (dir == Direction.LEFT)
            {
                if (transform.position.x <= prePos[0].pos.x)
                {
                    transform.position = prePos[0].pos;
                    SetAnim(prePos[0].dir);
                    Move();

                    prePos.RemoveAt(0);
                }
            }
            else if (dir == Direction.RIGHT)
            {
                if (transform.position.x >= prePos[0].pos.x)
                {
                    transform.position = prePos[0].pos;
                    SetAnim(prePos[0].dir);
                    Move();

                    prePos.RemoveAt(0);
                }
            }
        }
        else
        {
            if (isTurn) isTurn = false;
        }
    }

    public void Move()
    {
        if(rb2d == null) rb2d = GetComponent<Rigidbody2D>();

        if (dir == Direction.UP)
        {
            rb2d.velocity = Vector2.up * speed;
        }
        else if(dir == Direction.DOWN)
        {
            rb2d.velocity = Vector2.down * speed;
        }
        else if(dir == Direction.LEFT)
        {
            rb2d.velocity = Vector2.left * speed;
        }
        else
        {
            rb2d.velocity = Vector2.right * speed;
        }
    }

    public void SetAnim(Direction _dir)
    {
        dir = _dir;
        if (anit == null) anit = GetComponentInChildren<Animator>();

        if(dir == Direction.UP)
        {
            anit.SetTrigger("Up");
        }
        else if(dir == Direction.DOWN)
        {
            anit.SetTrigger("Down");
        }
        else if (dir == Direction.LEFT)
        {
            anit.SetTrigger("Left");
        }
        else
        {
            anit.SetTrigger("Right");
        }
    }
}
