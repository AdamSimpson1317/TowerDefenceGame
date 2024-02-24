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
    public int damage = 1;
    private bool wiz = false;
    public Rigidbody2D rb;
    Vector2 dir = Vector2.up;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        //Switch this to Archer and not Infantry when switch to Archer is made everywhere
        if(towerType == "Infantry")
        {
            ArrowTower();
        }
        else if(towerType == "Wizard")
        {
            WizardTower();
        }
    }

    void WizardTower()
    {
        Destroy(gameObject, destroyTimer);
        if(!wiz)
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
        wiz = true;
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
