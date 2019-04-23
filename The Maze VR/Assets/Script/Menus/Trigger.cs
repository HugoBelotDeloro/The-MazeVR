using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject triggeredObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            triggeredObject.SendMessage(name + "Enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            triggeredObject.SendMessage(name + "Exit");
        }
    }
}
