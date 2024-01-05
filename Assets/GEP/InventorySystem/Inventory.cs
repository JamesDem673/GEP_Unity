using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("UI")]
    public GameObject inventory;
    public List<Slot> InventorySlots = new List<Slot>();

    public void Start()
    {
        toggleInventory(false);

        foreach (Slot uiSlot in InventorySlots)
        {
            uiSlot.initialiseSlot();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            toggleInventory(!inventory.activeInHierarchy);
        }
    }

    public void addItemToInventory(Item itemToAdd)
    {
        int leftoverQuantity = itemToAdd.currentQuantity;
        Slot openSlot = null;
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            Item heltItem = InventorySlots[i].getItem();

            if (heltItem != null && itemToAdd.name == heltItem.name)
            {
                int freeSpaceInSlot = heltItem.maxQuantity - heltItem.currentQuantity;

                if (freeSpaceInSlot >= leftoverQuantity)
                {
                    heltItem.currentQuantity += leftoverQuantity;
                    Destroy(itemToAdd.gameObject);
                    return;
                }
                else
                {
                    heltItem.currentQuantity = heltItem.maxQuantity;
                    leftoverQuantity -= freeSpaceInSlot;
                }
            }
            else if (heltItem == null)
            {
                if (!openSlot)
                {
                    openSlot = InventorySlots[i];
                }
            }

            if (leftoverQuantity > 0 && openSlot)
            {
                openSlot.setItem(itemToAdd);
                itemToAdd.currentQuantity = leftoverQuantity;
                itemToAdd.gameObject.SetActive(false);
            }
            else
            {
                itemToAdd.currentQuantity = leftoverQuantity;
            }
        }

    }

    private void toggleInventory(bool enable)
    {
        inventory.SetActive(enable);

        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = enable;

    }
}
