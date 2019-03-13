using UnityEngine;

public class LoadSingleplayer : MonoBehaviour
{
    private static GameManager gameManager = GameManager.instance;

    public static void LoadSingleplayerScene(string scene)
    {
        gameManager.UnloadScene(gameManager.currentSceneName);
        gameManager.LoadScene(scene);
    }

    public void LoadWrapper(string scene)
    {
        LoadSingleplayerScene(scene);
    }
}
