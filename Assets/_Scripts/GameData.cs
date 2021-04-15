using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Author: Group 50
Vincent Tse - 301050515
Siying Li - 301054781
Derek Chan - 301021992
Last Modified: April 14, 2021
Description: Game data to save when saving game.
 */

[System.Serializable]
public class GameData
{
    // Start is called before the first frame update
    public int crystals;
    public int potions;

    public GameData(GameController game)
    {
        crystals = game.crystalCount;
        potions = game.potionCount;
    }
}
