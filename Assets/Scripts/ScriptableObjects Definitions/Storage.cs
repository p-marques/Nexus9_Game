using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Storage/New Storage")]
public class Storage : ScriptableObject
{
    private const int SPACE = 5;

    [SerializeField] private Item[] _items;

    [Tooltip("Event raised every time an item is added or removed")]
    [SerializeField] private NexusEvent _eventOnContentChanged;

    public IReadOnlyList<Item> Items => _items;

    public int Space => SPACE;

    private void OnEnable()
    {
        _items = new Item[SPACE];
    }

    public bool AddItem(Item inItem)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = inItem;

                if (_eventOnContentChanged) _eventOnContentChanged.Raise();

                return true;
            }
        }

        return false;
    }

    public void RemoveItem(Item inItem)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == inItem)
            {
                _items[i] = null;

                if (_eventOnContentChanged) _eventOnContentChanged.Raise();
            }
        }
    }

    public bool HasItem(Item inItem)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == inItem)
                return true;
        }

        return false;
    }
}
