using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaSceneLoader : MonoBehaviour
{
    public GameManager GameManager;
    public Scene EntranceScene;
    public Scene ExitScene;

    public bool PlayerInCorridor;
    

    public void EntranceEnter()
    {
        GameManager.LoadScene(ExitScene.name);
    }

    public void EntranceExit()
    {
        if (!PlayerInCorridor)
        {
            GameManager.UnloadScene(ExitScene.name);
        }
    }

    public void ExitEnter()
    {
        GameManager.LoadScene(EntranceScene.name);
    }

    public void ExitExit()
    {
        if (!PlayerInCorridor)
        {
            GameManager.UnloadScene(EntranceScene.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInCorridor = true;
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInCorridor = false;
    }
}
