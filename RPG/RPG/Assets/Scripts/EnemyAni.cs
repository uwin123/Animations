using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAni : MonoBehaviour
{
    public const string IDLE = "idle";
    public const string WALK = "walk";
    public const string ATTACK = "attack1_new";
    public const string DIE = "death1";

    Animation anim;

    void Start()
    {
        anim = GetComponentInChildren<Animation>();
    }

    public void ChangeAni(string aniName)
    {
        anim.CrossFade(aniName);
    }

    void Update()
    {
        
    }
}
