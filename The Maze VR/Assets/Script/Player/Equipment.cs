﻿using System.Collections.Generic;
using UnityEngine;

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

    public Inventory Inventory;
    
    public ItemSlot Compass;
    public ItemSlot Clothes;
    public ItemSlot Light;
    public Light Lamp;
    
    private Dictionary<ItemType, ItemSlot> _map;
    
    
    private void Start()
    {
        _map = new Dictionary<ItemType, ItemSlot>
        {
            {ItemType.Light, Light},
            {ItemType.Compass, Compass},
            {ItemType.Clothes, Clothes}
        };
    }

    private void UpdateItems()
    {
        Lamp.enabled = Light.Item.Type != ItemType.Null;
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
