using UnityEngine;

public class GoToMainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetAxis("Echap") > 0)
        {
            GameManager.Instance.LoadMainMenu();
        }
    }
}
