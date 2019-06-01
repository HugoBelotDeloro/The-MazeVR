using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{

    public GameObject PlayerButton;
    // Start is called before the first frame update
    public void load(List<string> players)
    {
        foreach (string player in players)
        {
            var button = Instantiate(PlayerButton, transform);
            button.GetComponent<LobbyPLayer>().player = player;
        }
    }

    public void add(string player)
    {
        var button = Instantiate(PlayerButton, transform);
        button.GetComponent<LobbyPLayer>().player = player;
    }
}
;
