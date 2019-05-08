using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

public class Equipment : MonoBehaviour
{
    public enum ItemType
    {
        Regular,
        Compass,
        Clothes,
        Light,
        Null
    }

    [SerializeField] private ItemSlot compassSlot;
    [SerializeField] private ItemSlot clothesSlot;
    [SerializeField] private ItemSlot lightSlot;
    [SerializeField] private Light lamp;
    [SerializeField] private GameObject compass;
    
    private Dictionary<ItemType, ItemSlot> _map;
    
    
    private void Start()
    {
        _map = new Dictionary<ItemType, ItemSlot>
        {
            {ItemType.Light, lightSlot},
            {ItemType.Compass, compassSlot},
            {ItemType.Clothes, clothesSlot}
        };
    }

    private void UpdateItems()
    {
        lamp.enabled = lightSlot.Item.Type != ItemType.Null;
        compass.SetActive(compassSlot.Item.Type != ItemType.Null);
    }
    
    public Item EquipItem(Item item)
    {
        ItemSlot slot;
        if (_map.TryGetValue(item.Type, out slot))
        {
            Item buffer = slot.Item;
            slot.Item = item;
            slot.ItemImage.sprite = item.Sprite;
            UpdateItems();
            return buffer;
        }
        return item;
    }
    
    /*public bool UnequipItem(ItemType itemType)
    {
        ItemSlot slot;
        if (_map.TryGetValue(itemType, out slot) && slot.Equals(default(ItemSlot)))
        {
            slot = new ItemSlot();
        }
    }*/
    
}
