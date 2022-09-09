using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main weapon class
[System.Serializable]
public class WeaponClass : ScriptableObject
{
    [Header("Info")]
    [SerializeField]string _weaponName;
    [ReadOnly][SerializeField]string _weaponClass;
    [SerializeField]string _weaponID;
    [SerializeField]protected GameObject _weaponModel;
    [Header("Stats")]
    [Min(1)]
    [SerializeField]protected int damage = 1;
    [Min(0.1f)]
    [SerializeField]protected float fireRate = 1;
    [Min(1f)]
    [SerializeField]protected float fireRange = 25;
    [SerializeField]protected float bulletSpeed = 25;
    [SerializeField]protected BulletObjectTypes bulletObjectType;
    [Tooltip("bullet object or particle generator object")]    
    [SerializeField]protected GameObject bulletObject;

    protected enum BulletObjectTypes
    {
        gameobject,
        particle, 
        melee
    }

    //____________________________________________
    private bool _isReady = true;
    public bool GetCDStatus() => _isReady;
    public void ChangeCDStatus() => _isReady = !_isReady;
    public string GetWeaponID() => _weaponID;
    public GameObject GetWeaponModel() => _weaponModel?_weaponModel:null;
    //____________________________________________
    public WeaponClass()
    {
        _weaponClass = this.GetType().Name;
    }
    //____________________________________________
    public virtual void Shoot(GameObject shootingPoint) => Debug.Log("<color=red>Error. No weapon class</color>");
    public IEnumerator StartCooldown()
    {
        ChangeCDStatus();
        yield return new WaitForSeconds(1/fireRate);
        ChangeCDStatus();
    }
}