using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public WorldMoney worldMoney;
    public WorldEnemies worldEnemies;
    public Transform initialSpawn;
    public GameObject[] enemyPrefabs;
    public List<int> enemyPrefabsIndex;
    public GameObject[] pathPoints;

    //This list acts as a counter for how many of each enemy type to spawn
    //E.g int<10,5,1> would be 10 basic, 5 medium and 1 hard enemy
    public List<int> enemyCounts;
    public float spawnDelay;
    public bool spawning;
    public int totalEnemies;
    
    [SerializeField]
    private bool spawnToggle;
    public bool waveCooldown;
    public Wave[] waves;
    public int waveCount = 0;
    

    private void Start()
    {
        //StartWave();
        StartCoroutine(SpawnNextWave());
    }

    private void Update()
    {
        //If all enemies dead and the game not in wave cooldown state
        if(worldEnemies.enemiesOnMap <= 0 && !waveCooldown)
        {
            waveCount++;
            //Next wave
            if (waveCount < waves.Length)
            {
                StartCoroutine(SpawnNextWave());
            }
        }
        
        //If enemies left to spawn and game in spawning state
        if (!spawnToggle && totalEnemies > 0 && spawning)
        {
            if (totalEnemies <= 0)
            {
                spawning = false;                
            }
            StartCoroutine(SpawnDelay());
            totalEnemies--;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartWave();
        }
    }
   
    public void StartWave()
    {
        
        PrepWave();
        CountTotalEnemies();
        spawning = true;
    }

    public void PrepWave()
    {
        totalEnemies = 0;

        Debug.Log(waves[waveCount].basicEnemy);
        Debug.Log(waves[waveCount].mediumEnemy);
        Debug.Log(waves[waveCount].hardEnemy);

        AddEnemyCounts(waves[waveCount].basicEnemy, 0);
        AddEnemyCounts(waves[waveCount].mediumEnemy, 1);
        AddEnemyCounts(waves[waveCount].hardEnemy, 2);
    }

    public void AddEnemyCounts(int enemies, int prefabIndex)
    {
        if (enemies > 0)
        {
            enemyCounts.Add(enemies);
            enemyPrefabsIndex.Add(prefabIndex);
        }
    }

    public void CountTotalEnemies()
    {
        totalEnemies = waves[waveCount].basicEnemy + waves[waveCount].mediumEnemy + waves[waveCount].hardEnemy;
        worldEnemies.enemiesOnMap = totalEnemies;
        Debug.Log(totalEnemies);
    }
        

    public IEnumerator SpawnDelay()
    {
        spawnToggle = true;
        SpawnEnemy();
        //Wait before spawning again    
        yield return new WaitForSeconds(spawnDelay);
        spawnToggle = false;
    }

    public void SpawnEnemy()
    {
        int randIndex = Random.Range(0, (enemyCounts.Count));
        Debug.Log("index:" + randIndex.ToString()+": "+enemyCounts.Count.ToString());
     
        GameObject enemy = Instantiate(enemyPrefabs[enemyPrefabsIndex[randIndex]], initialSpawn.position, initialSpawn.rotation);
        enemyCounts[randIndex]--;
        EnemyCountCheck(randIndex);
        

    }

    private bool EnemyCountCheck(int index)
    {
        if(enemyCounts[index] <= 0)
        {
            enemyCounts.RemoveAt(index);
            enemyPrefabsIndex.RemoveAt(index);
            return false;
        }
        return true;

    }

    public IEnumerator SpawnNextWave()
    {
        waveCooldown = true;
        worldEnemies.ToggleTimer(true);
        int timer = worldEnemies.waveTimer;
        if (waveCount > 0)
        {
            worldMoney.UpdateMoney(100);
        }
        for (int i = 0; i < timer; i++)
        {
            //Updates timer before wave starts
            worldEnemies.UpdateTimer(timer - i);
            yield return new WaitForSeconds(1f);
        }
        //Resets timer UI and hides it when wave starts
        worldEnemies.ResetTimer();
        worldEnemies.ToggleTimer(false);
        waveCooldown = false;
        StartWave();
    }



}

[System.Serializable]
public class Wave 
{
    public int basicEnemy;
    public int mediumEnemy;
    public int hardEnemy;
}



