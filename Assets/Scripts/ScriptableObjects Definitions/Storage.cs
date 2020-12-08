using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Storage")]
public class Storage : ScriptableObject
{
    private const int SPACE = 5;

    [SerializeField]
    private Item[] items;

    [SerializeField]
    [Tooltip("Event raised every time an item is added or removed")]
    private NexusEvent eventOnContentChanged;

    public IReadOnlyList<Item> Items => items;

    public int Space => SPACE;

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

                if (eventOnContentChanged) eventOnContentChanged.Raise();

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

                if (eventOnContentChanged) eventOnContentChanged.Raise();
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
