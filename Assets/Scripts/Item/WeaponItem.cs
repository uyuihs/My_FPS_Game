using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class WeaponItem : Item
{
    [Header("Animator Controller")]
    public AnimatorOverrideController weaponAnimator;

    public bool needIK = false;

    public float bulletRange = 100f;


}
