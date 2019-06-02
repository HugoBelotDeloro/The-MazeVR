using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPLayer : MonoBehaviour
{
    public string player;
    private static GameManager gameManager = GameManager.Instance;
    private void Start()
    {
        Text txt = gameObject.GetComponentInChildren<Text>();
        txt.text = player;
    }
    public void StartGame()
    {
        Debug.Log("Startgame " + player);
    }
}
