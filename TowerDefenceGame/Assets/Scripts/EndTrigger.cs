using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public WorldHealth worldHealth;

    void Start()
    {
        worldHealth = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WorldHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            worldHealth.UpdateHealth();
            Destroy(collision.gameObject);
        }
    }
}
