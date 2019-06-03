using UnityEngine;

public class PlzTPMe : MonoBehaviour
{
    public static PlzTPMe LastCallForHelp;
    
    private void Awake()
    {
        LastCallForHelp = this;
    }
}
