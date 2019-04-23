﻿using UnityEngine;

public class LoadSingleplayer : MonoBehaviour
{
    private static GameManager gameManager = GameManager.Instance;

    public static void LoadSingleplayerScene(string scene)
    {
        foreach (string activeScene in gameManager.ActiveScenes)
        {
            gameManager.UnloadScene(activeScene);
        }
        gameManager.LoadScene(scene);
    }

    public void LoadWrapper(string scene)
    {
        LoadSingleplayerScene(scene);
    }
}