using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private Inventory inventory;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (inventory.AddItem(item))
            {
                Destroy(this.gameObject);//En cas de tendances suicidaires, contactez un numéro d'aide
            }
        }
    }
}
