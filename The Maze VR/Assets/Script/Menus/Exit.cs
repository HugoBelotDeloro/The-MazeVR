using UnityEngine;

public class Exit : MonoBehaviour
{

    public void Quit()
    {
        Debug.Log(("Pressed exit button"));
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Quitted game");
    }
}
