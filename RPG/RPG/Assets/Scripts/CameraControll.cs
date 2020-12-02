using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Transform player;

    Vector3 offset;

    void Start()
    {
        offset = player.position - transform.position;
    }

    //카메라가 플레이어 움직임에 한 템포 늦게 움직임을 준다. 
    //그냥 Update하면, 카메라가 드드드드득하면서 움직이는 것 처럼 보임.  
    void LateUpdate()
    {
        //플레이어의 위치와 카메라의 위치를 최초 저장한 위치 차이만큼 자동으로 유지시켜주게 됨 
        transform.position = player.position - offset;
    }
}
