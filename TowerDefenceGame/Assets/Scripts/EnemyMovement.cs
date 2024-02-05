using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Move from 1 point to another
    //Once at the end trigger the health script and Destroy object

    public EnemyWave enemyWave;
    public GameObject[] corners;
    public float speed = 1f;
    public GameObject target;
    public int counter = 1;

    private void Start()
    {
        enemyWave = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyWave>();
        corners = enemyWave.pathPoints;
        target = corners[0];
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, target.transform.position) > 0.1f) 
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        
        else if(Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            target = corners[counter];
            counter++;
        }

    }

}
