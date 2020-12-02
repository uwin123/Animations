using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed = 180.0f;

    [System.NonSerialized] public int money = 100;
    void Start()
    {
        
    }
    public void SetCoinValue(int money)
    {
        this.money = money;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerParams>().AddMoney(money);
            SoundManager.instance.PlayPickUpSound();
            //Destroy(gameObject);
            RemoveFromWorld();
        }
    }

    public void RemoveFromWorld()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        transform.Rotate(0.0f, rotateSpeed * Time.deltaTime, 0.0f);
    }
}
