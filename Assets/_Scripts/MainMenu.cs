using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Vincent Tse.
 * 2021-02-13
 */

public class MainMenu : MonoBehaviour
{
    public DontDestroy dontDestroy;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void LoadGame()
    {
        dontDestroy.load = true;
        PlayGame();
        
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!!");
        Application.Quit();
    }
}
