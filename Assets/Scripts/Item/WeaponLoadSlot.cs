using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoadSlot : MonoBehaviour
{
    public GameObject currentWeaponModel;
    private void UnloadAndDestoryWeapon()
    {
        if (currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }
    }

    public void LoadWeaponModel(WeaponItem weapon)
    {
        UnloadAndDestoryWeapon();
        if (weapon == null) { return; }

        GameObject weaponModel = Instantiate(weapon.itemModel, transform);
        weaponModel.transform.localPosition = Vector3.zero;
        weaponModel.transform.localRotation = Quaternion.identity;
        weaponModel.transform.localScale = Vector3.one;
        currentWeaponModel = weaponModel;
    }
}
