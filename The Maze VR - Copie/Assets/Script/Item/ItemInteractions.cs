using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    [SerializeField] private Item item;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Inventory inventory = other.gameObject.GetComponentInChildren<Inventory>();
            if (inventory.AddItem(item))
            {
                Debug.Log("Goodbye");
                Destroy(gameObject);
            }
        }
    }
}
