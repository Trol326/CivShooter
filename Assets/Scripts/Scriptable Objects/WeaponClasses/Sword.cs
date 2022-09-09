using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Sword")]
public class Sword : WeaponClass
{
    public override void Shoot(GameObject shootingPoint)
    {
        Debug.Log("Sword");
    }
}