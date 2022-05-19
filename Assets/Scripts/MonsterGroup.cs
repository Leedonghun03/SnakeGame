using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroup : MonoBehaviour
{
    bool isTurn = false; //턴 상태 체크
    public float turnTime = 0; //다음 턴 시간 체크

    public float minLimitX = 0; //좌
    public float maxLimitX = 0; //우
    public float minLimitY = 0; //하
    public float maxLimitY = 0; //상

    public List<MonsterMoveController> monsterList = new List<MonsterMoveController>();

    public MonsterMoveController monsterHead;

    // Start is called before the first frame update
    void Start()
    {
        turnTime = Random.Range(2, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTurn)
        {
            turnTime -= Time.deltaTime;

            if(turnTime <= 0)
            {
                isTurn = true;
                RandomDirection();
            }

            MoveLimit();
        }
    }

    void ChangeMoveDirecrion(Direction _dir) //방향전환
    {
        monsterHead.SetAnim(_dir); //머리 몬스터 애니메이션 설정
        monsterHead.Move(); //머리 몬스터 이동

        if(monsterList.Count > 1) //꼬리 몬스터가 있다면 꼬리 몬스터에 이동과 방향 저장
        {
            for(int i = 1; i < monsterList.Count; i++)
            {
                PosDir temp;

                temp.pos = monsterHead.transform.position;
                temp.dir = _dir;

                monsterList[i].posPos.Add(temp);
            }
        }
    }

    void RandomDirection() //랜덤한 방향을 설정
    {
        Direction dir;

        while (true)
        {
            dir = (Direction)Random.Range(0, 4); //0~3까지 랜덤한 값을 Direction 타입으로 형변환

            if(dir == Direction.UP)
            {
                if (monsterHead.dir == Direction.UP || monsterHead.dir == Direction.DOWN || monsterHead.transform.position.y >= maxLimitY - 0.2f) continue; //이 조건은 위로 더 이상 올라갈 수 없다는 것
                else break;
            }
            else if(dir == Direction.DOWN)
            {
                if (monsterHead.dir == Direction.UP || monsterHead.dir == Direction.DOWN || monsterHead.transform.position.y <= minLimitY + 0.2f) continue; //이 조건은 아래로 더 이상 올라갈 수 없다는 것
                else break;
            }
            else if(dir == Direction.LEFT)
            {
                if (monsterHead.dir == Direction.LEFT || monsterHead.dir == Direction.RIGHT || monsterHead.transform.position.x <= minLimitX + 0.2f) continue; //이 조건은 왼쪽로 더 이상 올라갈 수 없다는 것
                else break;
            }
            else
            {
                if (monsterHead.dir == Direction.LEFT || monsterHead.dir == Direction.RIGHT || monsterHead.transform.position.x >= maxLimitX - 0.2f) continue; //이 조건은 왼쪽로 더 이상 올라갈 수 없다는 것
                else break;
            }
        }

        ChangeMoveDirecrion(dir);

        turnTime = Random.Range(2, 5);
        isTurn = false;
    }

    void MoveLimit() //이동 한계치에 다다랐을 때 이동방향 재설정
    {
        if(monsterHead.dir == Direction.UP)
        {
            if(monsterHead.transform.position.y >= maxLimitY)
            {
                isTurn = true;

                int dir = Random.Range(0, 2);

                if(dir == 0)
                {
                    ChangeMoveDirecrion(Direction.LEFT);
                }
                else
                {
                    ChangeMoveDirecrion(Direction.RIGHT);
                }

                turnTime = Random.Range(2, 5);
                isTurn = false; 
            }
        }
        else if(monsterHead.dir == Direction.DOWN)
        {
            if (monsterHead.transform.position.y <= minLimitY)
            {
                isTurn = true;

                int dir = Random.Range(0, 2);

                if (dir == 0)
                {
                    ChangeMoveDirecrion(Direction.LEFT);
                }
                else
                {
                    ChangeMoveDirecrion(Direction.RIGHT);
                }

                turnTime = Random.Range(2, 5);
                isTurn = false;
            }
        }
        else if(monsterHead.dir == Direction.LEFT)
        {
            if (monsterHead.transform.position.x <= minLimitX)
            {
                isTurn = true;

                int dir = Random.Range(0, 2);

                if (dir == 0)
                {
                    ChangeMoveDirecrion(Direction.UP);
                }
                else
                {
                    ChangeMoveDirecrion(Direction.DOWN);
                }

                turnTime = Random.Range(2, 5);
                isTurn = false;
            }
        }
        else
        {
            if (monsterHead.transform.position.x >= maxLimitX)
            {
                isTurn = true;

                int dir = Random.Range(0, 2);

                if (dir == 0)
                {
                    ChangeMoveDirecrion(Direction.UP);
                }
                else
                {
                    ChangeMoveDirecrion(Direction.DOWN);
                }

                turnTime = Random.Range(2, 5);
                isTurn = false;
            }
        }
    }
}
