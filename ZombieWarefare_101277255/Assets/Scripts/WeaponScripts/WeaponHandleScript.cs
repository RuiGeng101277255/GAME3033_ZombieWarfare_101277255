using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandleScript : MonoBehaviour
{
    [Header("Weapon To Hold"), SerializeField]
    GameObject weaponToSpawn;

    public PlayerController playerController;
    Animator playerWeaponAnimator;
    Sprite aimCrossSprite;
    WeaponComponentScript equippedWeapon;

    [SerializeField]
    GameObject weaponSocket;
    [SerializeField]
    Transform gripSocketLocationIK;

    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");

    bool wasFiring = false;
    bool firingPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerWeaponAnimator = GetComponent<Animator>();
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket.transform);
        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponentScript>();
        equippedWeapon.Initialize(this);
        gripSocketLocationIK = equippedWeapon.weaponGripLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        playerWeaponAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        playerWeaponAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gripSocketLocationIK.transform.position);
    }

    public void OnFire(InputValue value)
    {
        firingPressed = value.isPressed;

        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }
    }

    public void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInClip <= 0) return;

        playerWeaponAnimator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiring();
    }

    public void StopFiring()
    {
        playerWeaponAnimator.SetBool(isFiringHash, false);
        playerController.isFiring = false;
        equippedWeapon.StopFiring();
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        StartReloading();
    }

    public void StartReloading()
    {
        if (playerController.isFiring)
        {
            StopFiring();
        }
        if (equippedWeapon.weaponStats.totalBullets <= 0) return;

        playerWeaponAnimator.SetBool(isReloadingHash, true);
        equippedWeapon.StartReloading();

        InvokeRepeating(nameof(StopReloading), 0, 0.1f);
    }

    public void StopReloading()
    {
        if (playerWeaponAnimator.GetBool(isReloadingHash)) return;

        playerController.isReloading = false;
        equippedWeapon.StopReloading();
        playerWeaponAnimator.SetBool(isReloadingHash, false);
        CancelInvoke(nameof(StopReloading));
    }
}
