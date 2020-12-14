using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    private Storage playerInventory;

    [SerializeField]
    private GameObject layoutGroup;

    [SerializeField]
    private GameObject itemUIPrefab;

    private UIItemSlot[] itemSlots;

    private void Awake()
    {
        if (!playerInventory)
        {
            Debug.LogError($"UIInventory doesn't have a ref to the player's inventory.");
            return;
        }

        CreateInventorySlots();
    }

    public void ToggleVisibility()
    {
        layoutGroup.SetActive(!layoutGroup.activeInHierarchy);
    }

    public void UpdateContents()
    {
        Debug.Log("Contents of player's inventory has changed. Updating UI...");

        if (!playerInventory)
        {
            Debug.LogError($"UIInventory doesn't have a ref to the player's inventory.");
            return;
        }

        if (itemSlots.Length != playerInventory.Space)
        {
            Debug.LogError($"UIInventory item slots and player inventory space don't match.");
            return;
        }

        UpdateSlotsInfo();
    }

    private void CreateInventorySlots()
    {
        itemSlots = new UIItemSlot[playerInventory.Space];

        for (int i = 0; i < playerInventory.Space; i++)
        {
            itemSlots[i] = Instantiate(itemUIPrefab, layoutGroup.transform).GetComponent<UIItemSlot>();
        }

        UpdateSlotsInfo();
    }

    private void UpdateSlotsInfo()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].UpdateInfo(playerInventory.Items[i]);
        }
    }
}
