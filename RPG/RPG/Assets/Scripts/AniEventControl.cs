using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEventControl : MonoBehaviour
{
    void Start()
    {
        
    }
    public void SendAttackEnemy()
    {
        transform.parent.gameObject.SendMessage("AttackCalculate");
    }
    void Update()
    {
        
    }
}
