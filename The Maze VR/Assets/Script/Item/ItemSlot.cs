using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image ItemImage;
    public Item Item;

    void Start()
    {
        ItemImage = GetComponentInChildren<Image>();
    }
}
