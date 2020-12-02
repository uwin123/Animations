using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum State
    {
        Idle,       //정지
        Chase,      //추적
        Attack,     //공격
        Dead,       //사망
        NoState     //아무 일도 없는 상태 
    }

    public State currentState = State.Idle;

    EnemyParams myParams;
    EnemyAni myAni;
    Transform player;
    PlayerParams playerParams;

    CharacterController controller;

    float chaseDistance = 5.0f;             //플레이어를 향해 몬스터가 추적을 시작할 거리
    float attackDistance = 2.5f;            //플레이어가 안족으로 들어오게 되면 공격을 시작
    float reChaseDistance = 3.0f;           //플레이어가 도망 갈 경우 얼마나 떨어져야 다시 추적

    float rotAnglePerSecond = 360.0f;       //초당 회전 각도 
    float moveSpeed = 1.3f;                 //몬스터의 이동 속도 

    float attackDelay = 2.0f;
    float attackTimer = 0.0f;

    public ParticleSystem hitEffect;
    public GameObject selectMark;

    //리스폰 시킬 몬스터를 담을 변수 
    GameObject myRespawnObj;

    //리스폰 오브젝트에서 생성된 몇번째 몬스터에 대한 정보 
    public int spawnID { get; set; }

    //몬스터가 처음 생성될 때의 위치를 저장
    Vector3 originPos;

   
    

    void Start()
    {
        myAni = GetComponent<EnemyAni>();
        myParams = GetComponent<EnemyParams>();
        myParams.deadEvent.AddListener(CallDeadEvent);
        ChangeState(State.Idle, EnemyAni.IDLE);
        controller = GetComponent<CharacterController>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerParams = player.gameObject.GetComponent<PlayerParams>();

        hitEffect.Stop();
        HideSelection();
    }

    //몬스터가 리스폰 될 때 초기화 상태로 함
    public void AddToWorldAgain()
    {
        //리스폰 오브젝트에서 처음 생성될 때의 위치와 같게 함.
        transform.position = originPos;

        GetComponent<EnemyParams>().InitParams();
        GetComponent<BoxCollider>().enabled = true;
    }

    public void HideSelection()
    {
        selectMark.SetActive(false);
    }
    public void ShowSelection()
    {
        selectMark.SetActive(true);
    }

    //몬스터가 어느 리스폰 오브젝트로부터 만들어졌는지에 대한 정보를 전달 받을 함수 
    public void SetRespawnObj(GameObject respawnobj, int spawnID, Vector3 originPos)
    {
        myRespawnObj = respawnobj;
        this.spawnID = spawnID;
        this.originPos = originPos;
    }

    //몬스터가 죽는 순간 처리 명령어
    void CallDeadEvent()
    {
        ChangeState(State.Dead, EnemyAni.DIE);

        //몬스터가 죽은 후 아이템 및 동전을 생성한다. 
        ObjectManager.instance.DropCoinToPosition(transform.position, myParams.rewardMoney);
        player.gameObject.SendMessage("CurrentEnemyDead");

        //몬스터가 사망했을 때 나는 소리 
        SoundManager.instance.PlayEnemyDie();

        StartCoroutine(RemoveMeFromWorld());
    }

    IEnumerator RemoveMeFromWorld()
    {
        yield return new WaitForSeconds(1f);
        ChangeState(State.Idle, EnemyAni.IDLE);
        //리스폰 오브젝트에 자기 자신을 제거해 달라는 요청
        myRespawnObj.GetComponent<RespawnObj>().RemoveMonster(spawnID);
    }

    public void ShowHitEffect()
    {
        hitEffect.Play();
    }

    public void AttackCalculate()
    {
        playerParams.SetEnemyAttack(myParams.GetRandomAttack());
    }

    void UpdateState()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Chase:
                ChaseState();
                break;
            case State.Attack:
                AttackState();
                break;
            case State.Dead:
                DeadState();
                break;
            case State.NoState:
                NoState();
                break;
        }
    }

    public void ChangeState(State newState, string aniName)
    {
        if (currentState == newState)
            return;
        currentState = newState;
        myAni.ChangeAni(aniName);
    }

    void IdleState()
    {
        if (GetDistanceFromPlayer() < chaseDistance)
            ChangeState(State.Chase, EnemyAni.WALK);
    }

    void ChaseState()
    {
        //몬스터가 공격 가능 거리 안으로 들어가면 공격 상태
        if(GetDistanceFromPlayer() < attackDistance)
        {
            ChangeState(State.Attack, EnemyAni.ATTACK);
        }
        else
        {
            TurnToDestination();
            MoveToDestination();
        }
    }

    void AttackState()
    {
        if(GetDistanceFromPlayer() > reChaseDistance)
        {
            attackTimer = 0.0f;
            ChangeState(State.Chase, EnemyAni.WALK);
        }
        else
        {
            if(attackTimer > attackDelay)
            {
                transform.LookAt(player.position);
                myAni.ChangeAni(EnemyAni.ATTACK);

                attackTimer = 0.0f;

                //몬스터가 공격할 때 나는 소리 
                SoundManager.instance.PlayEnemyAttack();
            }
            attackTimer += Time.deltaTime;
        }
    }

    void DeadState()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    void NoState()
    {

    }

    void TurnToDestination()
    {
        Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotAnglePerSecond);
    }
    void MoveToDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
    

    //플레이어와 거리를 재는 함수 
    float GetDistanceFromPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance;
    }

    void Update()
    {
        UpdateState();
    }
}
