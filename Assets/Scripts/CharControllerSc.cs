using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControllerSc : MonoBehaviour
{
    [SerializeField]protected Animator characterAnimator;
    [SerializeField]protected GameObject weaponPoint;
    [SerializeField]protected WeaponClass currentWeapon;
    protected GameObject weaponObjRef;
    void Awake()
    {
        if(!characterAnimator)characterAnimator = GetComponent<Animator>();
        if(!weaponPoint)Debug.Log("<color=red>weapon point not assigned</color>", this);
    }

    public void SetCurrentWeapon(WeaponClass weapon)
    {
        currentWeapon = weapon;
        SpawnWeaponModel();
    }
    void SpawnWeaponModel()
    {
        if(!currentWeapon || !weaponPoint)return;
        if(!currentWeapon.GetWeaponModel())return;
        var weaponModel = currentWeapon.GetWeaponModel();
        weaponObjRef = Object.Instantiate(weaponModel, weaponPoint.transform);
    }
    //_______________________________________________________
    public void StartAttackAnimation()
    {
        characterAnimator.SetTrigger("isAttack");
    }
    //_______________________________________________________
    public virtual void UseAbility1()
    {
        // Dash
    }
    public virtual void UseAbility2()
    {
        //   
    }
}
