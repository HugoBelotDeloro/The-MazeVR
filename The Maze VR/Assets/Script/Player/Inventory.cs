using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public ItemSlot[] Items  = new ItemSlot[MaxItems];
    public GameObject[] ItemSlotObjects = new GameObject[MaxItems];
    private const int MaxItems = 3;
    public Item EmptyItem;
    public Image EmptyImage;
    public Equipment Equipment;

    private void Start()
    {
        for (int i = 0; i < MaxItems; i++)
        {
            ItemSlotObjects[i] = GameObject.Find("ItemSlot" + i);
            Items[i] = ItemSlotObjects[i].GetComponent<ItemSlot>();
        }
    }

    public bool AddItem(Item item)
    {
        if (item.Type != Equipment.ItemType.Regular)
        {
            return Equipment.EquipItem(item);
        }
        int i = 0;
        while (i < MaxItems && Items[i].Item.Type != Equipment.ItemType.Null)
        {
            i++;
        }
        if (i < MaxItems)
        {
            Items[i].Item = item;
            Items[i].ItemImage.sprite = item.Sprite;
            return true;
        }
        return false;
    }
    
    public bool RemoveItem(Item item)
    {
        int i = 0;
        while (i < MaxItems && Items[i].Item != item)
            i++;
        if (i != MaxItems)
        {
            Items[i].Item = EmptyItem;
            Items[i].ItemImage = EmptyImage;
            return true;
        }
        return false;
    }
}
