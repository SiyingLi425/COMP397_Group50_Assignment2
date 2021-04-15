using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Author: Group 50
Vincent Tse - 301050515
Siying Li - 301054781
Derek Chan - 301021992
Last Modified: April 14, 2021
Description: Saves certain player data to load later.
 */

[System.Serializable]
public class PlayerData 
{
    public int health;
    public int crystals;
    public int potions;
    public float[] position;
    public bool shield;
    public bool sword;

    public PlayerData (PlayerBehaviour player)
    {
        health = player.currentHealth;
        position = new float[3];
        position[0] = player.controller.transform.position.x;
        position[1] = player.controller.transform.position.y;
        position[2] = player.controller.transform.position.z;
        sword = player.gotSword;
        shield = player.gotShield;

    }
}
