using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Author: Group 50
Vincent Tse - 301050515
Siying Li - 301054781
Derek Chan - 301021992
Last Modified: April 14, 2021
Description: Saves information about an item.
 */
public class Item
{
    public enum ItemType
    {
        Sword,
        HealthPotion,
        StarFragment
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.StarFragment: return ItemAssets.Instance.starFragmentSprite;

        }
    }
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: 
                return false;
            case ItemType.HealthPotion:
            case ItemType.StarFragment:
                return true;
        }
    }

}
