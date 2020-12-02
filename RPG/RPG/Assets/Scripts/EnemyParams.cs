﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                       //유니티 UI를 생성 할 때 추가하는 네임 스페이스.

public class EnemyParams : CharacterParams
{
    public string newname;
    public int exp { get; set; }
    public int rewardMoney { get; set; }
    public Image hpBar;

    public override void InitParams()
    {
        name = "Monster";
        level = 1;
        maxHp = 50;
        curHp = maxHp;
        attackMin = 10;
        attackMax = 20;
        defense = 1;

        exp = 10;
        rewardMoney = Random.Range(10, 31);
        isDead = false;

        InitHpBarSize();

    }

    void InitHpBarSize()
    {
        //hp Bar의 사이즈를 원래 자신의 사이즈, 1배의 사이즈로 초기화 시켜 주게 된다. 
        hpBar.rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
    protected override void UpdateAfterReceiveAttack()
    {
        base.UpdateAfterReceiveAttack();

        hpBar.rectTransform.localScale = new Vector3((float)curHp / (float)maxHp, 1.0f, 1.0f);
    }
}
