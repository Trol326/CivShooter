using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunner : EnemyClass
{
    [Min(0.1f)]
    [SerializeField]protected float fireRate = 1;
    [SerializeField]protected float bulletSpeed = 25;
    [Min(1)]
    [SerializeField]protected int maxAmmo = 1;
    protected int currentAmmo;
    [Min(1)]
    [SerializeField]protected float reloadTime = 1;
    [Header("Game objects")]
    [SerializeField]protected GameObject shootingPoint;
    [SerializeField]protected GameObject bulletObject;
    protected override void Awake()
    {
        base.Awake();
        currentAmmo = maxAmmo;
    }
    protected override void Attack()
    {
        if (GetCDStatus())
        {
            if (currentAmmo > 0)
            {
                AimToPlayer();
                var obj = Object.Instantiate(bulletObject, shootingPoint.transform.position + (shootingPoint.transform.forward * 2), shootingPoint.transform.rotation);
                obj.GetComponent<BulletSc>().ShootBullet(attackRange, shootingPoint.transform.position, bulletSpeed, enemyDamage, false);
                currentAmmo--;
                StartCoroutine(StartCooldown(1/fireRate));
            }
            else
            {
                currentAmmo = maxAmmo;
                StartCoroutine(StartCooldown(reloadTime));
            }
        }
    }

    protected void AimToPlayer()
    {
        // TODO: Make more smooth
        shootingPoint.transform.LookAt(player.transform); 
    }
}
