using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/FetherGun")]
public class FetherGun : WeaponClass 
{
    public override void Shoot(GameObject shootingPoint)
    {
        Debug.Log("Fether");
        var angle = Quaternion.Euler(new Vector3(shootingPoint.transform.eulerAngles.x, shootingPoint.transform.eulerAngles.y, shootingPoint.transform.eulerAngles.z));
        var obj = Object.Instantiate(bulletObject, shootingPoint.transform.position+(shootingPoint.transform.forward*2), angle);
        obj.GetComponent<BulletSc>().ShootBullet(fireRange, shootingPoint.transform.position, bulletSpeed, damage);
    }
}