using UnityEngine;

public class LoadMultiplayer : MonoBehaviour
{
    private static GameManager gameManager = GameManager.instance;

    public static void LoadMultiplayerScene(string scene)
    {
        gameManager.UnloadScene(gameManager.currentSceneName);
        gameManager.LoadScene(scene);
    }

    public void LoadWrapper(string scene)
    {
        LoadMultiplayerScene(scene);
    }
}
