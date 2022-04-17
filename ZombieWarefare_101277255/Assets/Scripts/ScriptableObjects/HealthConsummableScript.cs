using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/HealthPotion", order = 1)]
public class HealthConsummableScript : ConsummableScriptableObject
{
    public override void UseItem(PlayerController playerController)
    {
        if (playerController.healthScript.CurrentHealth >= playerController.healthScript.MaxHealth) return;

        playerController.healthScript.HealDamage(itemEffect);

        base.UseItem(playerController);
    }
}
