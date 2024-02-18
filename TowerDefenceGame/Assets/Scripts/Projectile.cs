using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string type;
    private Transform target;
    public float speed = 70f;
    public int damage = 1;
    private bool wiz;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(type == "Archer")
        {
            ArrowTower();
        }
        else if(type == "Wizard")
        {
            WizardTower();
        }
    }

    void WizardTower()
    {
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            wiz = true;
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(wiz && collision.transform.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
