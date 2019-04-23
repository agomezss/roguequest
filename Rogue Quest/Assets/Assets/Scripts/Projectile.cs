using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public GameObject Shooter;
    public float Damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            if ((Shooter.CompareTag("Player") && other.CompareTag("Enemy")) ||
                (Shooter.CompareTag("Enemy") && other.CompareTag("Player")))
            {
                ApplyDamage(other.gameObject);
                Destroy(gameObject);
            }
        }
    }

    void ApplyDamage(GameObject obj)
    {
        var enemyStats = obj.GetComponent<Stats>();

        if (enemyStats)
            enemyStats.GetDamage(Damage);
    }

}