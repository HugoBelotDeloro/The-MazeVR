using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
    public GameObject gameManager;
    
    void Awake()
    {
        if (GameManager.Instance == null)
        {
            Instantiate(gameManager);
        }
    }
}
