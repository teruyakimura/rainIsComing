﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Reload(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit(string screen)
    {
        SceneManager.LoadScene(screen);
    }

}
