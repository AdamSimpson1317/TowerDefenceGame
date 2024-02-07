using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldHealth : MonoBehaviour
{
    public int health = 200;
    public TextMeshProUGUI healthText;
    public void UpdateHealth()
    {
        health--;
        healthText.text = health.ToString();
    }
}
