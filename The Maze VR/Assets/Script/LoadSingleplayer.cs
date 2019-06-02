using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSingleplayer : MonoBehaviour
{
    private static GameManager gameManager = GameManager.Instance;

    public static void LoadSingleplayerScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void LoadWrapper(string scene)
    {
        LoadSingleplayerScene(scene);
    }
}
