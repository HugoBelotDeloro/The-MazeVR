using UnityEngine;

public class LoadSingleplayer : MonoBehaviour
{
    private static GameManager gameManager = GameManager.Instance;

    public static void LoadSingleplayerScene(string scene)
    {
        gameManager.UnloadScene("MainMenu");
        gameManager.LoadScene(scene);
    }

    public void LoadWrapper(string scene)
    {
        LoadSingleplayerScene(scene);
    }
}
