using UnityEngine;

public class LoadMultiplayer : MonoBehaviour
{
    private static GameManager gameManager = GameManager.Instance;

    public static void LoadMultiplayerScene(string scene)
    {
        gameManager.UnloadScene(gameManager.CurrentSceneName);
        gameManager.LoadScene(scene);
    }

    public void LoadWrapper(string scene)
    {
        LoadMultiplayerScene(scene);
    }
}
