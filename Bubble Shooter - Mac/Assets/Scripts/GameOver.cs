using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    /* Changes to Game Options screen when "Play Again" option is selected */
    public void PlayGame()
    {
        SceneManager.LoadScene("Testing 1");
    }

    /* Changes to Game Modes screen when "Game Modes" is selected*/
    public void GameModes()
    {
        SceneManager.LoadScene("GameModeScene");  
    }
}
