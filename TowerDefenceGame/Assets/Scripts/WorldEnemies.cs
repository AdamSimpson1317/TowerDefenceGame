using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldEnemies : MonoBehaviour
{
    public int enemiesOnMap;
    public int waveTimer;

    public GameObject timerUI;
    public TextMeshProUGUI timerTextUI;

    public void ToggleTimer(bool toggle)
    {
        timerUI.SetActive(toggle);
    }
    public void ResetTimer()
    {
        timerTextUI.text = waveTimer.ToString();
    }
    public void UpdateTimer(int time)
    {
        timerTextUI.text = time.ToString();
    }


}
