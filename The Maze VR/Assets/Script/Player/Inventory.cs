using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private ItemSlot[] _items;
    [SerializeField] private int maxItems;
    [SerializeField] private Item emptyItem;
    [SerializeField] private Image emptyImage;
    [SerializeField] private Equipment equipment;
    [SerializeField] private ItemSlot emptyItemSlot;

    private void Start()
    {
        equipment.gameObject.SetActive(true);
        _items = new ItemSlot[maxItems];
        for (int i = 0; i < maxItems; i++)
        {
            _items[i] = Instantiate(emptyItemSlot, transform);
        }
    }

    public bool AddItem(Item item)
    {
        if (item.Type != Equipment.ItemType.Regular)
        {
            Item t = equipment.EquipItem(item);
            return t.Type == Equipment.ItemType.Null;
        }
        int i = 0;
        while (i < maxItems && _items[i].item.Type != Equipment.ItemType.Null)
        {
            i++;
        }
        if (i < maxItems)
        {
            _items[i].item = item;
            _items[i].itemImage.sprite = item.Sprite;
            return true;
        }
        return false;
    }
    
    public bool RemoveItem(Item item)
    {
        int i = 0;
        while (i < maxItems && _items[i].item != item)
            i++;
        if (i != maxItems)
        {
            _items[i].item = emptyItem;
            _items[i].itemImage = emptyImage;
            return true;
        }
        return false;
    }
}
