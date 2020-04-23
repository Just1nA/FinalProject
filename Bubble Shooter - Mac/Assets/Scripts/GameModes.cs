using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModes : MonoBehaviour
{
    /* Sets the scene up for the Classic Level */
    public void PlayClassic()
    {
        /* Sets the scene up for the Classic Level */
        PlayerPrefs.SetInt("ROWSHIFTER", 999999999);
        PlayerPrefs.SetInt("ROWSPAWN", 7);
        SceneManager.LoadScene("Testing 1");
    }

    /* Changes to the Main Menu screen when "Back" option is chosen */
    public void GoBack()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    /* Sets the scene up for the Hard Level */
    public void PlayHard()
    {
        PlayerPrefs.SetInt("ROWSHIFTER", 6);
        PlayerPrefs.SetInt("ROWSPAWN", 9);
        SceneManager.LoadScene("Testing 1");
    }
}
