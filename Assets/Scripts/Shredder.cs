using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D projectile)
    {
        var isPlayerProjectile = projectile.gameObject.layer == 8;
        var isEnemyProjectile = projectile.gameObject.layer == 9;
        
        if (isPlayerProjectile || isEnemyProjectile)
        {
            Destroy(projectile.gameObject);   
        }
    }
}
