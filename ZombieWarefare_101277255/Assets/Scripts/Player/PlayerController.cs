using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool isFiring;
    public bool isReloading;
    public bool isRunning;
    public bool isJumping;
    public bool isAiming;
    public bool isInventoryOpen;

    public InventoryComponent inventory;
    public GameUIController uiController;

    private void Awake()
    {
        if (inventory == null)
        {
            inventory = GetComponent<InventoryComponent>();
        }

        if (uiController == null)
        {
            uiController = FindObjectOfType<GameUIController>();
        }
    }

    public void OnInventory(InputValue value)
    {
        isInventoryOpen = !isInventoryOpen;
        OpenInventory(isInventoryOpen);
    }

    private void OpenInventory(bool b)
    {
        if (b)
        {
            uiController.EnableInventoryMenu();
        }
        else
        {
            uiController.EnableGameMenu();
        }
    }
}
