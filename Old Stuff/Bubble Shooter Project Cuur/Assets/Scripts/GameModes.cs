﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModes : MonoBehaviour
{
    public void PlayClassic()
    {
        //Load scene of game through here
        SceneManager.LoadScene("Testing 1");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}