using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    [SerializeField] private Item item;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponentInChildren<Inventory>().AddItem(item))
        {
            Destroy(gameObject);
        }
    }
}
