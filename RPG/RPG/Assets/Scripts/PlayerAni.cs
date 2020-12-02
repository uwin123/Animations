using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    //애니메이터 컨트롤러의 전이 관계에서 설정한 번호에 맟춘다.
    public const int ANI_IDLE = 0;
    public const int ANI_WALK = 1;
    public const int ANI_ATTACK = 2;
    public const int ANI_ATKIDLE = 3;
    public const int ANI_DIE = 4;

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //애니메이션 번호를 입력 받아 플레이어의 애니메이션을 해당되는 애니메이션으로 바꿔주는 함수 
    public void ChangeAni(int aniNumber)
    {
        anim.SetInteger("AniName", aniNumber);
    }

    void Update()
    {
       
    }
}
