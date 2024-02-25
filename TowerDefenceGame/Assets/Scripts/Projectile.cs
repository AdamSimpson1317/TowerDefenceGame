using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string towerType;
    private Transform target;
    public float speed = 70f;
    //DestroyTimer Could be an Upgrade.
    public float destroyTimer = 5f;
    public int projectileSizeMultiplier;
    public int damage = 1;
    private bool infan = false;
    private bool isWiz = false;
    public Rigidbody2D rb;
    Vector2 dir = Vector2.up;

    private void Start()
    {
        projectileSizeMultiplier = 8;
    }
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(towerType == "Archer")
        {
            ArrowTower();
        }
        else if(towerType == "Infantry")
        {
            InfantryTower();
        }
        if(towerType == "Wizard")
        {
            WizardTower();
        }
    }

    void WizardTower()
    {
        Destroy(gameObject, 0.5f);
        if(target == null)
        {
            //Destroy(gameObject);
            return;
        }
        
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame && !isWiz)
        {
            HitTargetExplosion();
            transform.localScale  *= projectileSizeMultiplier;
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void InfantryTower()
    {
        Destroy(gameObject, destroyTimer);
        if(!infan)
        {
            dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if(dir.magnitude <= distanceThisFrame)
            {
                HitTargets(dir);
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
    }

    void ArrowTower()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        target.GetComponent<EnemyHealth>().TakeDamage(damage);
        Destroy(gameObject);
    }
    void HitTargets(Vector2 dir)
    {
        target.GetComponent<EnemyHealth>().TakeDamage(damage);
        infan = true;
    }

    void HitTargetExplosion()
    {
        target.GetComponent<EnemyHealth>().TakeDamage(damage);
        isWiz = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(target != null)
        {
            if(collision.gameObject != target.gameObject)
            {
                collision.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
        else if(target == null)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
