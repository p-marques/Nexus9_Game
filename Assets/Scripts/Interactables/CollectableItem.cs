using UnityEngine;

public class CollectableItem : MonoBehaviour, IInteractableItem
{
    private const float RANGE = 2f;
    private const string INTERACTION_TEXT_ANALYSE = "Analyse";

    [SerializeField]
    private Item item;

    [SerializeField]
    private ItemNexusEvent onAnalyseEvent;

    private bool hasBeenAnalysed;

    public string InteractionText => hasBeenAnalysed ? item.ItemName : INTERACTION_TEXT_ANALYSE;

    public float Range => RANGE;

    public Item Item => item;

    public bool HasBeenAnalysed
    {
        get => hasBeenAnalysed;
        set
        {
            hasBeenAnalysed = value;
        }
    }

    private void Awake()
    {
        if (!item)
            Debug.LogError($"{name} doesn't have a item set to be picked up.");
    }

    public void Interact(Player player)
    {
        if (!hasBeenAnalysed)
        {
            onAnalyseEvent.Raise(this);

            return;
        }

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
