using UnityEngine;

public class LoadMultiplayer : MonoBehaviour
{
    private static GameManager gameManager = GameManager.Instance;

    public static void LoadMultiplayerScene(string scene)
    {
        foreach (string activeScene in gameManager.ActiveScenes)
        {
            gameManager.UnloadScene(activeScene);
        }
        gameManager.LoadScene(scene);
    }

    public void LoadWrapper(string scene)
    {
        LoadMultiplayerScene(scene);
    }
}
