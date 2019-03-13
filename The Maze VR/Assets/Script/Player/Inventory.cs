using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] itemImages = new Image[MaxItems];
    public Item[] items  = new Item[MaxItems];
    public const int MaxItems = 3;

    public bool AddItem(Item item)
    {
        int i = 0;
        while (i < MaxItems && items[i] != null)
        {
            i++;
        }
        if (items[i] == null)
        {
            items[i] = item;
            itemImages[i].sprite = item.sprite;
            itemImages[i].enabled = true;
            return true;
        }
        return false;
    }
    
    public bool RemoveItem(Item item)
    {
        int i = 0;
        while (i < MaxItems && items[i] != item)
            i++;
        if (i != MaxItems)
        {
            items[i] = null;
            itemImages[i].sprite = null;
            itemImages[i].enabled = false;
            return true;
        }
        return false;
    }
    
}
