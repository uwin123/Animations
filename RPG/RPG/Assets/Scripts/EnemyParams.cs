using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParams : CharacterParams
{
    public string newname;
    public int exp { get; set; }
    public int rewardMoney { get; set; }
    public override void InitParams()
    {
        name = "Monster";
        level = 1;
        maxHp = 50;
        curHp = maxHp;
        attackMin = 2;
        attackMax = 5;
        defense = 1;

        exp = 10;
        rewardMoney = Random.Range(10, 31);
        isDead = false;

    }
    protected override void UpdateAfterReceiveAttack()
    {
        base.UpdateAfterReceiveAttack();
    }
}
