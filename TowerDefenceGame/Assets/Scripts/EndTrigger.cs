using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public WorldHealth worldHealth;
    WorldEnemies worldEnemies;

    void Start()
    {
        worldHealth = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WorldHealth>();
        worldEnemies = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WorldEnemies>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            worldHealth.UpdateHealth();
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(10);
        }
    }
}
