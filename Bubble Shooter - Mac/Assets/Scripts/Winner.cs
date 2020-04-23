using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Winner : MonoBehaviour
{
    /* Changes to Game Options screen when "Play Again" option is selected */
    public void PlayAgainGame()
    {
        SceneManager.LoadScene("Testing 1");
    }

    /* Exits out of the Game when "Quit" option is selected*/
    public void GameModes()
    {
        SceneManager.LoadScene("GameModeScene");  
    }
}
