using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Data/SceneData")]
public class SceneDataSO : ScriptableObject
{
    [Header("Player Data")]
    public int health;
    public int crystals;
    public int potions;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public bool shield;
    public bool sword;
}
