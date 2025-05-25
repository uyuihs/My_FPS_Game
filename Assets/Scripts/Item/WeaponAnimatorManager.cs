using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour
{
    Animator weaponAnimator;
    [Header("Weapon FX")]
    public GameObject weaponMuzzleFlashFX;
    public GameObject weaponBulletCaseFx;

    [Header("Weapon FX Transforms")]
    public Transform weaponMuzzleFlashTransform;
    public Transform weaponBulletCaseTransform;

    [Header("Shoot DetectLayer")]
    public LayerMask shootableLayers;

    private void Awake()
    {
        weaponAnimator = GetComponentInChildren<Animator>();
    }

    public void ShootWeapon(PlayerCamera playerCamera)
    {
        //播放武器的动画
        // weaponAnimator.Play("Fire");

        //特效
        GameObject muzzleFlash = Instantiate(weaponMuzzleFlashFX, weaponMuzzleFlashTransform);

        muzzleFlash.transform.parent = null;
        //子弹
        GameObject bulletCase = Instantiate(weaponBulletCaseFx, weaponBulletCaseTransform);

        bulletCase.transform.parent = null;

        RaycastHit hit;

        if (Physics.Raycast(playerCamera.cameraObject.transform.position, playerCamera.transform.forward, out hit, shootableLayers))
        {
            Debug.Log(hit.collider.gameObject.layer);
        }
    }
}
