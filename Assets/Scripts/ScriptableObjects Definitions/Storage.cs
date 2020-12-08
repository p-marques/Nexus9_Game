using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Storage")]
public class Storage : ScriptableObject
{
    private const int SPACE = 5;

    [SerializeField]
    private Item[] items;

    public IReadOnlyList<Item> Items => items;

    private void OnEnable()
    {
        items = new Item[SPACE];
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

    public bool HasItem(Item inItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == inItem)
                return true;
        }

        return false;
    }
}
