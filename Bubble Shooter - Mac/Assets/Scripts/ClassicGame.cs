using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassicGame : MonoBehaviour
{
    /* Changes to Main Menu of the game */
    public void Back()
    {
        SceneManager.LoadScene("GameModeScene");
    }
}
