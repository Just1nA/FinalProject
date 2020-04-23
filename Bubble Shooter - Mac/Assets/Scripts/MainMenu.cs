using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int HighScore;
    
    void Awake()
    {
        if (PlayerPrefs.GetInt("HighScore") > 0) PlayerPrefs.SetInt("HighScore", 0);
    }

    /* Changes to Game Options screen when "Play" option is selected */
    public void PlayGame()
    {
        SceneManager.LoadScene("GameModeScene");
    }
    /* Exits out of the Game when "Quit" option is selected*/
    public void QuitGame()
    {
        Application.Quit();  
    }

}
