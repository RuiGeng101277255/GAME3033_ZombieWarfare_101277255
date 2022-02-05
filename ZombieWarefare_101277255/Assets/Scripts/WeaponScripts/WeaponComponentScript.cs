using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponentScript : MonoBehaviour
{
    public Transform weaponGripLocation;

    protected WeaponHandleScript weaponHandle;

    [SerializeField]
    public WeaponStats weaponStats;

    public bool isFiring = false;
    public bool isReloading = false;

    protected Camera mainCamera;
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(WeaponHandleScript handle)
    {
        weaponHandle = handle;
    }

    public virtual void StartFiring()
    {
        isFiring = true;

        if (weaponStats.repeating)
        {
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiring()
    {
        isFiring= false;
        CancelInvoke(nameof(FireWeapon));
    }

    protected virtual void FireWeapon()
    {
        print("firing");
        weaponStats.bulletsInClip--;
        //switch statement based on the firing pattern
    }
}

public enum WeaponType
{
    NONE,
    PISTOL,
    RIFLE,
    GRANADES
}

public enum WeaponFiringPattern
{
    SINGLE_SHOT,
    THREE_BURST,
    SEMI_AUTO,
    FULL_AUTO
}

[System.Serializable]
public struct WeaponStats
{
    public string weaponName;
    public float weaponDamage;
    public int bulletsInClip;
    public int clipSize;
    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool repeating;
    public LayerMask weaponHitLayers;
    public WeaponFiringPattern firingPattern;
    public WeaponType weaponType;
}