using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveController : MonoBehaviour
{
    public int idx = 0;

    public bool isHead = false;
    public bool isTurn = false;
    public Direction dir = Direction.UP;

    public List<PosDir> posPos = new List<PosDir>();

    public float speed = 1;

    Rigidbody2D rb2d;
    Animator anit;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anit = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if(posPos.Count != 0)
        {
            isTurn = true;

            if(dir == Direction.UP)
            {
                if(transform.position.y >= posPos[0].pos.y)
                {
                    transform.position = posPos[0].pos;
                    SetAnim(posPos[0].dir);
                    Move();

                    posPos.RemoveAt(0);
                }
            }
            else if(dir == Direction.DOWN)
            {
                if (transform.position.y <= posPos[0].pos.y)
                {
                    transform.position = posPos[0].pos;
                    SetAnim(posPos[0].dir);
                    Move();

                    posPos.RemoveAt(0);
                }
            }
            else if (dir == Direction.LEFT)
            {
                if (transform.position.x <= posPos[0].pos.x)
                {
                    transform.position = posPos[0].pos;
                    SetAnim(posPos[0].dir);
                    Move();

                    posPos.RemoveAt(0);
                }
            }
            else
            {
                if (transform.position.x >= posPos[0].pos.x)
                {
                    transform.position = posPos[0].pos;
                    SetAnim(posPos[0].dir);
                    Move();

                    posPos.RemoveAt(0);
                }
            }
        }
    }

    public void SetAnim(Direction _dir) //방향에 따른 애니메이션 설정
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
        else if (dir == Direction.RIGHT)
        {
            anit.SetTrigger("Right");
        }
    }

    public void Move() //이동
    {
        if (rb2d == null) rb2d = GetComponent<Rigidbody2D>();

        if (dir == Direction.UP)
        {
            rb2d.velocity = Vector2.up * speed;
        }
        else if (dir == Direction.DOWN)
        {
            rb2d.velocity = Vector2.down * speed;
        }
        else if (dir == Direction.LEFT)
        {
            rb2d.velocity = Vector2.left * speed;
        }
        else if (dir == Direction.RIGHT)
        {
            rb2d.velocity = Vector2.right * speed;
        }
    }

}
