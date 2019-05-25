using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.GetComponent<PlayerHealth>().Damage(1000);
            Destroy(gameObject);
        }
    }
}
