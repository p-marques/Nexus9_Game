using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Storage _playerInventory;

    [SerializeField] private GameObject _layoutGroup;

    [SerializeField] private GameObject _itemUIPrefab;

    private UIItemSlot[] _itemSlots;

    private void Awake()
    {
        if (!_playerInventory)
        {
            Debug.LogError($"UIInventory doesn't have a ref to the player's inventory.");
            return;
        }

        CreateInventorySlots();
    }

    public void ToggleVisibility()
    {
        _layoutGroup.SetActive(!_layoutGroup.activeInHierarchy);
    }

    public void UpdateContents()
    {
        Debug.Log("Contents of player's inventory has changed. Updating UI...");

        if (!_playerInventory)
        {
            Debug.LogError($"UIInventory doesn't have a ref to the player's inventory.");
            return;
        }

        if (_itemSlots.Length != _playerInventory.Space)
        {
            Debug.LogError($"UIInventory item slots and player inventory space don't match.");
            return;
        }

        UpdateSlotsInfo();
    }

    private void CreateInventorySlots()
    {
        _itemSlots = new UIItemSlot[_playerInventory.Space];

        for (int i = 0; i < _playerInventory.Space; i++)
        {
            _itemSlots[i] = Instantiate(_itemUIPrefab, _layoutGroup.transform).GetComponent<UIItemSlot>();
        }

        UpdateSlotsInfo();
    }

    private void UpdateSlotsInfo()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].UpdateInfo(_playerInventory.Items[i]);
        }
    }
}
