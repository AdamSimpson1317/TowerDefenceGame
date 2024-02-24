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

    public Wave[] waves;
    public int waveCount = 0;


    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        if(worldEnemies.enemiesOnMap <= 0)
        {
            waveCount++;
            //Next wave
            if (waveCount < waves.Length)
            {
                StartCoroutine(SpawnNextWave());
            }
        }
        
        if (!spawnToggle && totalEnemies > 0 && spawning)
        {
            totalEnemies--;
            if (totalEnemies <= 0)
            {
                spawning = false;                
            }
            StartCoroutine(SpawnDelay());
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

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPrefabsIndex.Add(i);
        }
        

        enemyCounts.Add(waves[waveCount].basicEnemy);
        enemyCounts.Add(waves[waveCount].mediumEnemy);
        enemyCounts.Add(waves[waveCount].hardEnemy);
    }

    public void CountTotalEnemies()
    {
        totalEnemies = waves[waveCount].basicEnemy + waves[waveCount].mediumEnemy + waves[waveCount].hardEnemy;
        worldEnemies.enemiesOnMap = totalEnemies;
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
        GameObject enemy = Instantiate(enemyPrefabs[enemyPrefabsIndex[randIndex]], initialSpawn.position, initialSpawn.rotation);
        enemyCounts[randIndex]--;
        EnemyCountCheck(randIndex);
        



    }

    private void EnemyCountCheck(int index)
    {
        if(enemyCounts[index] <= 0)
        {
            enemyCounts.RemoveAt(index);
            enemyPrefabsIndex.RemoveAt(index);
            
        }

    }

    public IEnumerator SpawnNextWave()
    {
        worldMoney.UpdateMoney(100);
        yield return new WaitForSeconds(10f);
       
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



