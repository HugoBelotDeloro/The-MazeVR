using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string CurrentSceneName;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
            CurrentSceneName = "MainMenu";
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        CurrentSceneName = scene;
    }
    
    public void UnloadScene(string scene)
    {
        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload(string scene)
    {
        yield return null;
        SceneManager.UnloadSceneAsync(scene);
    }
}