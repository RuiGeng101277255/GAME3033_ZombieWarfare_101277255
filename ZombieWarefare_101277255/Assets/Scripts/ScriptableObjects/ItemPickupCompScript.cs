using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupCompScript : MonoBehaviour
{
    [SerializeField]
    ItemScript pickupItem;

    [Tooltip("Manual Override for amount, -1 it will use scriptable object's amount")]
    [SerializeField]
    int itemAmount = -1;

    [SerializeField] MeshRenderer propMeshRenderer;
    [SerializeField] MeshFilter propMeshFilter;

    ItemScript itemInstance;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateItem();
    }

    void InstantiateItem()
    {
        itemInstance = Instantiate(pickupItem);
        if (itemAmount > 0)
        {
            itemInstance.SetAmount(itemAmount);
        }
        else
        {
            itemInstance.SetAmount(pickupItem.amountValue);
        }
        ApplyMesh();
    }

    void ApplyMesh()
    {
        if (propMeshFilter) propMeshFilter.mesh = pickupItem.itemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        if (propMeshRenderer) propMeshRenderer.materials = pickupItem.itemPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        InventoryComponent inventoryComponent = other.GetComponent<InventoryComponent>();

        WeaponHandleScript handle = other.GetComponent<WeaponHandleScript>();

        if (inventoryComponent)
        {
            inventoryComponent.AddItem(itemInstance, itemAmount);
        }

        if (itemInstance.itemCategory == ItemCategory.Weapon)
        {
            WeaponComponentScript tempWeaponComp = itemInstance.itemPrefab.GetComponent<WeaponComponentScript>();

            //if ((handle.weaponAmmoData.ContainsKey(tempWeaponComp.weaponStats.weaponType)) && (other.GetComponentInChildren<WeaponHandleScript>().equippedWeapon))
            if (handle.weaponAmmoData.ContainsKey(tempWeaponComp.weaponStats.weaponType))
            {
                WeaponStats tempStats = handle.weaponAmmoData[tempWeaponComp.weaponStats.weaponType];
                tempStats.totalBullets += itemInstance.amountValue;
                other.GetComponentInChildren<WeaponHandleScript>().weaponAmmoData[tempWeaponComp.weaponStats.weaponType] = tempStats;
                //other.GetComponentInChildren<WeaponHandleScript>().equippedWeapon.weaponStats.totalBullets += pickupItem.amountValue;

                if (handle.equippedWeapon != null)
                {
                    handle.equippedWeapon.weaponStats = handle.weaponAmmoData[tempWeaponComp.weaponStats.weaponType];
                }
            }
        }

        Destroy(gameObject);
    }
}
