using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 2)]
public class WeaponScriptableObject : EquippableScriptableScript
{
    public WeaponStats weaponStats;

    public override void UseItem(PlayerController playerController)
    {
        if (equipped)
        {
            //unequip here and remove weapon from controller
        }
        else
        {
            //invoke OnWeaponEquipped and equip weapon from weapon holder on playercontroller
        }

        base.UseItem(playerController);
    }
}
