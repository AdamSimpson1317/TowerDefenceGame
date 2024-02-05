using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    
    public Transform initialSpawn;
    public List<GameObject> enemyPrefabs;
    public GameObject[] pathPoints;

    //This list acts as a counter for how many of each enemy type to spawn
    //E.g int<10,5,1> would be 10 basic, 5 medium and 1 hard enemy
    public List<int> enemyCounts;
    public float spawnDelay;
    public bool spawning;
    public int totalEnemies;
    
    [SerializeField]
    private bool spawnToggle;


    private void Start()
    {
        for (int i = 0; i < enemyCounts.Count; i++)
        {
            totalEnemies += enemyCounts[i];
        }
    }

    private void Update()
    {
        
        if (!spawnToggle && totalEnemies > 0)
        {
            totalEnemies--;
            StartCoroutine(SpawnDelay());
        }   
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
        GameObject enemy = Instantiate(enemyPrefabs[randIndex], initialSpawn.position, initialSpawn.rotation);
        enemyCounts[randIndex]--;
        EnemyCountCheck(randIndex);



    }

    private void EnemyCountCheck(int index)
    {
        if(enemyCounts[index] <= 0)
        {
            enemyCounts.RemoveAt(index);
            enemyPrefabs.RemoveAt(index);
            
        }
       
    }



}


