using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    {
        itemSlotContainer = transform.Find("Items");
        itemSlotTemplate = itemSlotContainer.Find("InventorySlot");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItem();
    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventoryItem();
    }

    private void RefreshInventoryItem()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        foreach (Item item in inventory.GetItemList())
        {
            float itemSlotCellSize = 30f;
            RectTransform itemSlotRectTransform =  Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("ItemButton").Find("icon").GetComponent<Image>();
            image.sprite = item.GetSprite();

            Text uiText = itemSlotRectTransform.Find("ItemButton").Find("amount").GetComponent<Text>();
            if(item.amount > 1)
            {
                uiText.text = item.amount.ToString();
            }
            else
            {
                uiText.text = "";
            }
            


            x++;
            if(x > 4)
            {
                x = 0;
                y++;
            }
        }
    }
}
