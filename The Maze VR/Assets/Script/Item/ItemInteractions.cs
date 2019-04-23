using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    public Item Item;
    public Inventory Inventory;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Inventory.AddItem(Item);
            Destroy(gameObject);
        }
    }
}
