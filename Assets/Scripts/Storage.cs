using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private const int space = 5;

    [SerializeField]
    private Item[] items;

    public Item[] Items => items;

    [SerializeField]
    private Item tempItem0;
    [SerializeField]
    private Item tempItem1;

    private void Awake()
    {
        items = new Item[space];
        items[0] = tempItem0;
        items[1] = tempItem1;
    }

    public bool AddItem(Item inItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = inItem;
                return true;
            }
        }

        return false;
    }

    public void RemoveItem(Item inItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == inItem)
            {
                items[i] = null;
            }
        }
    }
}
