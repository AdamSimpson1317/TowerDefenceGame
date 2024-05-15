using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShooting : MonoBehaviour
{
    private Transform target;
    public float range = 2f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public string enemyTag = "Enemy";
    public GameObject projectilePrefab;
    public Transform firePoint;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
            return;

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject projectileGo = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGo.GetComponent<Projectile>();

        if(projectile != null)
            projectile.Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
