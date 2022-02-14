using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents
{
    public delegate void OnWeaponEquippedEvent(WeaponComponentScript weaponComponent);

    public static event OnWeaponEquippedEvent OnWeaponEquipped;

    public static void InvokeOnWeaponEquipped(WeaponComponentScript weaponComponent)
    {
        OnWeaponEquipped?.Invoke(weaponComponent);
    }
}
