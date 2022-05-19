using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public float spawnRate = 5;
    public float spawnTime = 0;

    public float minLimitX = 0;
    public float maxLimitX = 0;
    public float minLimitY = 0;
    public float maxLimitY = 0;

    public GameObject group;
    public GameObject monster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;

        if(spawnTime >= spawnRate)
        {
            SpawnMonster();
            spawnTime = 0;
        }
    }

    void SpawnMonster() //몬스터 소환
    {
        Direction dir = (Direction)Random.Range(0, 4); //방향 설정

        Vector2 pos = new Vector2(Random.Range(minLimitX, maxLimitX), Random.Range(minLimitY, maxLimitY)); //몬스터 위치 설정

        GameObject groupTemp = Instantiate(group); //그룹 오브젝트 생성

        groupTemp.transform.SetParent(transform); //그룹 오브젝트 부모 설정

        int num = Random.Range(1, 4); //생성될 몬스터 개체수 랜덤하게 반환

        for(int i = 0; i < num; i++)
        {
            GameObject mon = Instantiate(monster); //몬스터 생성
            mon.transform.SetParent(groupTemp.transform); //몬스터 부모 설정. 그룹 오브젝트에 넣기
            mon.GetComponent<MonsterMoveController>().idx = i; //몬스터 인덱스 설정

            if(i == 0) //몬스터가 헤드
            {
                mon.transform.position = pos; //몬스터에 위치 대입
                mon.GetComponent<MonsterMoveController>().isHead = true; //첫 생성 몬스터 헤드 지정
                groupTemp.GetComponent<MonsterGroup>().monsterHead = mon.GetComponent<MonsterMoveController>();
                groupTemp.GetComponent<MonsterGroup>().monsterHead.SetAnim(dir);
                groupTemp.GetComponent<MonsterGroup>().monsterHead.Move();
            }
            else //꼬리 몬스터들
            {
                MonsterMoveController perMon = groupTemp.GetComponent<MonsterGroup>().monsterList[i - 1]; //현재 생성된 몬스터 이전 몬스터 받아옴

                Vector2 preMonPos = perMon.transform.position; //이전 몬스터 위치

                if(perMon.dir == Direction.UP)
                {
                    mon.transform.position = new Vector2(preMonPos.x, preMonPos.y - 0.25f); //생성된 몬스터에 이전 몬스터위치에서 오프셋만큼 차이를 두고 대입
                }
                else if(perMon.dir == Direction.DOWN)
                {
                    mon.transform.position = new Vector2(preMonPos.x, preMonPos.y + 0.25f);
                }
                else if(perMon.dir == Direction.LEFT)
                {
                    mon.transform.position = new Vector2(preMonPos.x + 0.25f, preMonPos.y);
                }
                else
                {
                    mon.transform.position = new Vector2(preMonPos.x - 0.25f, preMonPos.y);
                }

                for(int j = 0; j < perMon.posPos.Count; j++)
                {
                    mon.GetComponent<MonsterMoveController>().posPos.Add(perMon.posPos[j]);
                }

                mon.GetComponent<MonsterMoveController>().SetAnim(perMon.dir);
                mon.GetComponent<MonsterMoveController>().Move();
            }

            groupTemp.GetComponent<MonsterGroup>().monsterList.Add(mon.GetComponent<MonsterMoveController>());
        }

    }


}
