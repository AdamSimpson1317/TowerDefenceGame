using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldMoney : MonoBehaviour
{
    public int money = 200;
    public TextMeshProUGUI moneyText;
    public void UpdateMoney(int amount)
    {
        money += amount;
        moneyText.text = money.ToString();
    }
}
