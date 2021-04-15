using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
Author: Group 50
Vincent Tse - 301050515
Siying Li - 301054781
Derek Chan - 301021992
Last Modified: April 14, 2021
Description: Controls Quests and achievements given when completed quests.
 */

public class QuestAchievementController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Crystal Quest")]
    public int crystalGoal = 4;
    public int crystalCount = 0;
    public Text crystalText;


    [Header("Beginner Quest")]
    public bool gotWeapon, gotPotion, gotCrystal, defeatedEnemy, completedBeginnerQuest;
    public Toggle weaponToggle, potionToggle, crystalToggle, defeatedEnemyToggle;

    [Header("Dialogue")]
    public Dialogue dialogue;

    public GameController gameController;

    void Start()
    {
        gameController = GetComponent<GameController>();
        crystalText.text = crystalCount + "/" + crystalGoal;
    }

    // Update is called once per frame
    void Update()
    {
        if (gotWeapon && gotPotion && gotCrystal && defeatedEnemy && !completedBeginnerQuest)
        {
            Debug.Log("COmpleted beginner quest");
            gameController.addPotion(10);
            completedBeginnerQuest = true;
            TriggerDialogue();
        }
    }

    public void setCrystal(int num)
    {
        crystalCount = num;
        crystalText.text = crystalCount + "/" + crystalGoal;

        if (crystalCount >= crystalGoal)
        {
            Debug.Log("Level Cleared");
            SceneManager.LoadScene("Win");
        }
    }

    public void toggleWeapon()
    {
        if (!gotWeapon)
        {
            weaponToggle.isOn = true;
            gotWeapon = true;
        }

    }

    public void toggleCrystal()
    {
        if (!gotCrystal)
        {
            crystalToggle.isOn = true;
            gotCrystal = true;
        }

    }

    public void togglePotion()
    {
        if (!gotPotion)
        {
            potionToggle.isOn = true;
            gotPotion = true;
        }

    }

    public void toggleEnemy()
    {
        if (!defeatedEnemy)
        {
            defeatedEnemyToggle.isOn = true;
            defeatedEnemy = true;
        }

    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, this.gameObject);
    }
}
