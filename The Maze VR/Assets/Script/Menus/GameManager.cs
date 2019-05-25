using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string mainScene;
    public List<string> ActiveScenes = new List<string>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            LoadScene("MainMenu");
            mainScene = "MainMenu";
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        ActiveScenes.Add(scene);
        if (mainScene is null)
        {
            mainScene = scene;
        }
    }
    
    public void UnloadScene(string scene)
    {
        if (scene == mainScene)
        {
            mainScene = null;
            foreach (string s in ActiveScenes)
            {
                StartCoroutine(Unload(s));
            }
        }
        else
        {
            StartCoroutine(Unload(scene));
        }
    }

    IEnumerator Unload(string scene)
    {
        yield return null;
        if (ActiveScenes.Contains(scene))
        {
            ActiveScenes.Remove(scene);
            SceneManager.UnloadSceneAsync(scene);
        }
    }

    public void ResetScene()
    {
        string[] activeScenes = ActiveScenes.ToArray();
        foreach (string scene in activeScenes)
        {
            Debug.Log(scene);
            SceneManager.UnloadSceneAsync(scene);
        }
        foreach (string scene in activeScenes)
        {
            SceneManager.LoadScene(scene);
        }
    }
}