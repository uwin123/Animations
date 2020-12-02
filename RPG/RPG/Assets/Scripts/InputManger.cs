using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManger : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void CheckClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //카메라 로부터 화면사의 좌표를 관통하는 가상의 선(레이)를 생성해서 리턴해 주는 함수.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 
            //Physics.RayCast(레이 타입 변수, out 레이 캐스트 히트 타입변수);
            //가상의 레이져선(레이)이 충돌체와 충돌하게 되면, true(참)값을 ㄹ리턴하여 ㄷ동시에 레이캐스트 히트 변수에 충
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.gameObject.name == "Terrain")
                {
                    //player.transform.position = hit.point;

                    //마우스 클릭 지점의 좌표를 플레이어가 전달 받은 뒤, 상태를 이동상태로 바뀜 
                    player.GetComponent<PlayerFSM>().MoveTo(hit.point);
                }
                else if(hit.collider.gameObject.tag == "Enemy") //마우스 클릭한 대상이 적 캐릭터인 경우,
                {
                    player.GetComponent<PlayerFSM>().AttackEnemy(hit.collider.gameObject);
                }
            }
        }
    }
    void Update()
    {
        CheckClick();
    }
}
