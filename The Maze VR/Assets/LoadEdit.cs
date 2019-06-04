using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEdit : MonoBehaviour
{
    private static GameManager gameManager = GameManager.Instance;

    public static void LoadEditScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }


    public void LoadWrapper(string scene)
    {
        LoadEditScene(scene);
    }
}
