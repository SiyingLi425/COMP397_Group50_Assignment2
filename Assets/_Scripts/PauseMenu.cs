using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Linq;
/*
Author: Group 50
Vincent Tse - 301050515
Siying Li - 301054781
Derek Chan - 301021992
Last Modified: April 14, 2021
Description: manages menu popup when pause button is pressed, and pauses the game.
 */
public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;

    public PlayerBehaviour player;
    public GameController game;
    

    public SceneDataSO sceneData;
    private DontDestroy dontDestroy;


    private void Start()
    {
        dontDestroy = FindObjectOfType<DontDestroy>();
        if (dontDestroy && dontDestroy.load)
        {
            LoadGamePressed();
        }
    }

    /*public void TogglePause()
    {
        GamePaused = !GamePaused;

        if (GamePaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (!GamePaused)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }*/

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        
    }

    public void SaveGamePressed()
    {
        Debug.Log("Saving game");
        sceneData.playerPosition = player.transform.position;
        sceneData.health = player.currentHealth;
        sceneData.potions = game.potionCount;
        sceneData.crystals = game.crystalCount;
        sceneData.playerRotation = player.transform.rotation;
        sceneData.sword = player.gotSword;
        sceneData.shield = player.gotShield;
    }

    public void LoadGamePressed()
    {
        player.controller.enabled = false;
        player.transform.position = sceneData.playerPosition;
        player.transform.rotation = sceneData.playerRotation;
        player.controller.enabled = true;

        player.currentHealth = sceneData.health;
        player.healthBar.SetHealth(player.currentHealth);

        if (sceneData.sword)
        {

            player.sword.SetActive(true);
            player.gotSword = true;
        }
        player.gotShield = sceneData.shield;
        if (player.gotShield)
        {
            player.shield.SetActive(true);

        }
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GamePaused = false;
        SceneManager.LoadScene("Menu");
    }

    
}
