using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMultiplayer : MonoBehaviour
{
    private static GameManager gameManager = GameManager.Instance;

    public static void LoadMultiplayerScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }


    public void LoadWrapper(string scene)
    {
        LoadMultiplayerScene(scene);
    }
}
