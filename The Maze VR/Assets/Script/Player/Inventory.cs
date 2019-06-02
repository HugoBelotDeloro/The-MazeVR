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
    [SerializeField] private ItemSlot keySlot;
    [SerializeField] private Text keyAmount;

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
        if (item.Type == Equipment.ItemType.Key)
        {
            PutItem(item, keySlot);
            keySlot.stack++;
            keyAmount.text = keySlot.stack.ToString();
            return true;
        }
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
            PutItem(item, _items[i]);
            return true;
        }
        return false;
    }

    public bool UseKey()
    {
        if (keySlot.stack > 0)
        {
            if (--keySlot.stack == 0)
            {
                PutItem(emptyItem, keySlot);
            }
            keyAmount.text = keySlot.stack.ToString();
            return true;
        }
        return false;
    }

    private void PutItem(Item item, ItemSlot slot)
    {
        slot.item = item;
        slot.itemImage.sprite = item.Sprite;
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
