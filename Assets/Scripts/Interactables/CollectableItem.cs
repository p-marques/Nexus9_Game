using UnityEngine;

public class CollectableItem : MonoBehaviour, IInteractable
{
    private const float RANGE = 2f;

    [SerializeField]
    private Item item;

    [SerializeField]
    private string interactionText;

    public string InteractionText => interactionText;

    public float Range => RANGE;

    private void Awake()
    {
        if (!item)
            Debug.LogError($"{name} doesn't have a item set to be picked up.");
    }

    public void Interact(Player player)
    {
        Storage playerInventory = player.Inventory;

        if (playerInventory)
        {
            if (playerInventory.AddItem(item))
            {
                Destroy(gameObject);
                
                Debug.Log($"Player picked up \"{item.ItemName}\".");
            }
            else
                Debug.LogWarning($"Player failed to pick up {item.ItemName}. Inventory full?"); // TODO: Notify UI
        }
        else
            Debug.LogError("Can't pick up item because there is no reference to the player's inventory.");
    }
}
