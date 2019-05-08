using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private Inventory inventory;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventory.AddItem(item);
            Destroy(gameObject);
        }
    }
}
