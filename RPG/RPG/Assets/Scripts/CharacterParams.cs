﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;           //유니티 이벤트를 사용하기 위해서는 네임스페이스를 추가해야함. 

//CharacterParams는 플레이어의 파라미터 클래스와 몬스터 파라미터 클래스의 부모 클래스 역할을 하게 된다. 
public class CharacterParams : MonoBehaviour
{
    //퍼블릭 변수가 아니라 약식 프로퍼티, 속성으로 지정 
    //퍼블릭 변수와 똑같이 사용할 수 있으나, 유니티 인스펙터에 노출되는 것을 막고 보안을 위해 정식 프로퍼티로 전환이 쉬워진다. 
    public int level { get; set; }
    public int maxHp { get; set; }
    public int curHp { get; set; }
    public int attackMin { get; set; }
    public int attackMax { get; set; }
    public int defense { get; set; }
    public bool isDead { get; set; }

    [System.NonSerialized] public UnityEvent deadEvent = new UnityEvent();

    private void Start()
    {
        InitParams();
    }

    //나중에 CharacterParams 클래스를 상속한 자식 클래스에서 InitParams함수에 자신만의 명령어를 추가하기만 하면 자동으로 필요한 명령어들이 실행. 
    public virtual void InitParams()
    {

    }
    public int GetRandomAttack()
    {
        int randAttack = Random.Range(attackMin, attackMax + 1);
        return randAttack;
    }
    public void SetEnemyAttack(int enemyAttackPower)
    {
        curHp -= enemyAttackPower;
        UpdateAfterReceiveAttack();
    }
    //캐릭터가 적으로부터 공격을 받은 뒤에 자동으로 실행될 함수를 가상함수로 만듬.
    protected virtual void UpdateAfterReceiveAttack()
    {
        print(name + "'s HP." + curHp);

        if(curHp <= 0)
        {
            curHp = 0;
            isDead = true;
            deadEvent.Invoke();
        }
    }
}
