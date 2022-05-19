using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public static HeroManager hm;

    const int MaxHeroLength = 9;
    int heroLength = 1;

    public float inputRate = 0.15f;
    public float inputTime = 0;

    bool isCanInput = false;

    public GameObject[] prefabHero;
    public HeroMoveController headHero;
    public List<HeroMoveController> heroList = new List<HeroMoveController>();

    private void Awake()
    {
        hm = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCanInput)
        {
            inputTime += Time.deltaTime;

            if(inputTime >= inputRate)
            {
                inputTime = 0;
                isCanInput = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (headHero.dir == Direction.UP || headHero.dir == Direction.DOWN) return;
                
                ChangeMoveDirection(Direction.UP);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (headHero.dir == Direction.UP || headHero.dir == Direction.DOWN) return;
                
                ChangeMoveDirection(Direction.DOWN);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (headHero.dir == Direction.LEFT || headHero.dir == Direction.RIGHT) return;

                ChangeMoveDirection(Direction.LEFT);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (headHero.dir == Direction.LEFT || headHero.dir == Direction.RIGHT) return;
                
                ChangeMoveDirection(Direction.RIGHT);
            }
        }
    }

    void ChangeMoveDirection(Direction _dir)    //히어로의 방향 변경
    {
        headHero.SetAnim(_dir); //애니메이션과 방향 설정
        headHero.Move();    //이동

        if (heroList.Count > 1)  //꼬리 히어로가 있다면
        {
            for (int i = 0; i < heroList.Count; i++)
            {
                PosDir temp;

                temp.pos = headHero.transform.position; //현재 헤드 히어로 위치 대입
                temp.dir = _dir;    //현재 헤드 히어로 방향 대입

                heroList[i].prePos.Add(temp);   //모든 꼬리 히어로 위치리스트에 저장
            }
        }

        isCanInput = false;
    }

    public void AddHero(int id) //히어로 추가 함수
    {
        HeroMoveController preHero = heroList[heroLength - 1];

        Vector2 pos = Vector2.down; //추가될 히어로 포지션 변수
        Vector2 heroPos = preHero.transform.position; //추가될 히어로 앞의 히어로

        if(preHero.dir == Direction.UP)
        {
            pos = new Vector2(heroPos.x, heroPos.y - 0.25f);
        }
        else if(preHero.dir == Direction.DOWN)
        {
            pos = new Vector2(heroPos.x, heroPos.y + 0.25f);
        }
        else if(preHero.dir == Direction.LEFT)
        {
            pos = new Vector2(heroPos.x +0.25f, heroPos.y);
        }
        else
        {
            pos = new Vector2(heroPos.x - 0.25f, heroPos.y);
        }

        GameObject hero = Instantiate(prefabHero[id], pos, Quaternion.identity); //히어로 생성
        hero.transform.SetParent(transform.GetChild(0).transform); //부모 오브젝트
        hero.GetComponent<HeroMoveController>().idx = heroLength; //인덱스
        hero.GetComponent<HeroMoveController>().dir = preHero.dir; //방향 설정

        for (int i = 0; i < preHero.prePos.Count; i++) //방향과 위치가 담긴 리스트를 생성한 히어로 리스트에 담아준다.
        {
            hero.GetComponent<HeroMoveController>().prePos.Add(preHero.prePos[i]);
        }

        hero.GetComponent<HeroMoveController>().SetAnim(preHero.dir); //방향에 따른 애니메이션 설정
        hero.GetComponent<HeroMoveController>().Move(); //이동

        heroLength++; //히어로 길이 추가
        heroList.Add(hero.GetComponent<HeroMoveController>()); //히어로 리스트에 새 히어로 추가
    }

}
