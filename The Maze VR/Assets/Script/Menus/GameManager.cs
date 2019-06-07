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

    private void SetMainScene(string scene)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
        mainScene = scene;
    }
    
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            LoadScene("MainMenu");
        }
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
        ActiveScenes.Add(scene);
    }

    private IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        yield return new WaitUntil(() => loading.isDone);
        SetMainScene(scene);
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

    public AsyncOperation UnloadSingleScene(string sceneName)
    {
        return SceneManager.UnloadSceneAsync(sceneName);
    }

    public void SwitchScenes(string sceneName)
    {
        UnloadScene(mainScene);
        LoadScene(sceneName);
    }

    private IEnumerator Unload(string scene)
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
        foreach (string scene in ActiveScenes)
        {
            SceneManager.UnloadScene(scene);
        }
        ActiveScenes = new List<string>();
        LoadScene(mainScene);
    }

    public void LoadMainMenu()
    {
        UnloadScene(mainScene);
        LoadScene("MainMenu");
    }
}