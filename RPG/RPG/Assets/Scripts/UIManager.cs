using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //언제 어디서나 쉽게 접근할 수 있도록 하기 위해 만든 정적 변수
    public static UIManager instance;

    public Text playerName;
    public Text playerMoney;
    public Image playerHPBar;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void UpdatePlayerUI(PlayerParams playerParams)
    {
        playerName.text = playerParams.name;
        playerMoney.text = "Coin : " + playerParams.money.ToString();
        playerHPBar.rectTransform.localScale = new Vector3((float)playerParams.curHp / (float)playerParams.maxHp, 1.0f, 1.0f);
    }

    void Update()
    {
        
    }
}
