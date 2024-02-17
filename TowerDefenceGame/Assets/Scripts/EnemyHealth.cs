using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    WorldMoney worldMoney;
    public int maxHealth;
    public int health;

    public SpriteRenderer spriteRenderer;
    public Color redColour;
    public Color yellowColour;
    public Color blackColour;
    public EnemyMovement movement;

    void Start()
    {
        health = maxHealth;
        worldMoney = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WorldMoney>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        ChangeColour();
        if(health == 0)
        {
            worldMoney.UpdateMoney(10);
            Destroy(gameObject);
        }

    }

    private void ChangeColour()
    {
        if(health == 1)
        {
            spriteRenderer.color = redColour;
            movement.speed = 1f;
        }
        else if (health == 2)
        {
            spriteRenderer.color = yellowColour;
            movement.speed = 3f;
        }
        else if (health == 3)
        {
            spriteRenderer.color = blackColour;
            movement.speed = 0.75f;
        }
    }
}
