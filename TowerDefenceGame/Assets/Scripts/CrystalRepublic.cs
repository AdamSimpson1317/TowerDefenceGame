using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRepublic : MonoBehaviour
{
    EnemyWave enemyWave;
    WorldMoney worldMoney;
    public int nextWaveMoney;
    public int moneyGiven;


    // Start is called before the first frame update
    void Start()
    {
        enemyWave = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyWave>();
        worldMoney = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WorldMoney>();

        nextWaveMoney = enemyWave.waveCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyWave.waveCooldown &&  nextWaveMoney < enemyWave.waveCount)
        {
            nextWaveMoney++;
            worldMoney.UpdateMoney(moneyGiven);
            
        }
    }
}
