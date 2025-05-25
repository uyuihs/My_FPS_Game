using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    PlayerAnimationManager animationManager;
    WeaponLoadSlot weaponLoadSlot;
    [Header("Current Equipment")]
    public WeaponItem weapon;
    public WeaponAnimatorManager weaponAnimatorManager; 
    RightHandTargetIK rightIK;
    LeftHandTargetIK leftIK;
    //public WeaponItem subweapon;

    private void Awake()
    {
        animationManager = GetComponent<PlayerAnimationManager>();
        LoadWeaponLoaderSlots();
    }

    private void Start()
    {
        LoadCurrentWeapon();
    }

    private void LoadWeaponLoaderSlots()
    {
        //背部插槽
        weaponLoadSlot = GetComponentInChildren<WeaponLoadSlot>();
    }

    private void LoadCurrentWeapon()
    {
        //加载武器到玩家手里
        //重载动画控制器
        weaponLoadSlot.LoadWeaponModel(weapon);
        animationManager.animator.runtimeAnimatorController = weapon.weaponAnimator;
        if (weapon.needIK)
        {
            LoadIK();
        }
        weaponAnimatorManager = weaponLoadSlot.currentWeaponModel.GetComponentInChildren<WeaponAnimatorManager>();
    }

    private void LoadIK()
    {
        rightIK = weaponLoadSlot.currentWeaponModel.GetComponentInChildren<RightHandTargetIK>();
        leftIK = weaponLoadSlot.currentWeaponModel.GetComponentInChildren<LeftHandTargetIK>();
        animationManager.AssignHandIK(rightIK, leftIK);
    }
}
