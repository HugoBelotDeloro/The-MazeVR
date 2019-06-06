using UnityEngine;

public class LoadMultiplayer : MonoBehaviour
{
    private static GameManager gameManager = GameManager.Instance;

    private static void LoadMultiplayerScene(string scene)
    {
        gameManager.UnloadScene(gameManager.mainScene);
        gameManager.LoadScene(scene);
    }


    public void LoadWrapper(string scene)
    {
        LoadMultiplayerScene(scene);
    }
}
