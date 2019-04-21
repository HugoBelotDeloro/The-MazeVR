using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    public Item Item;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            inventory.AddItem(Item);
            Destroy(gameObject);
        }
    }
}
