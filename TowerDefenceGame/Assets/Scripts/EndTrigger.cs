using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public WorldHealth worldHealth;

    // Start is called before the first frame update
    void Start()
    {
        worldHealth = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WorldHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.tag);
        if(collision.transform.CompareTag("Enemy"))
        {
            worldHealth.UpdateHealth();
            Destroy(collision.gameObject);
        }
    }
}
