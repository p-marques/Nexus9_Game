using UnityEngine;

public class CollectableItem : MonoBehaviour, IInteractableItem
{
    private const float RANGE = 2f;
    private const string INTERACTION_TEXT_ANALYSE = "Analyse";

    [SerializeField]
    private Item item;

    [Header("Events")]
    [SerializeField]
    [Tooltip("Event raised on analyse start")]
    private ItemNexusEvent onAnalyseEvent;

    [SerializeField]
    [Tooltip("Event raised when player can't pick up item")]
    private NexusEvent onActionBlocked;

    private bool hasBeenAnalysed;
    private Player playerRef;

    public string InteractionText => hasBeenAnalysed ? item.ItemName : INTERACTION_TEXT_ANALYSE;

    public float Range => RANGE;

    public Item Item => item;

    public bool HasBeenAnalysed
    {
        get => hasBeenAnalysed;
        set
        {
            if (value)
            {
                playerRef.IsAnalysingItem = false;
            }

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
        playerRef = player;

        if (!hasBeenAnalysed)
        {
            onAnalyseEvent.Raise(this);

            player.IsAnalysingItem = true;

            return;
        }

        Storage playerInventory = player.Inventory;

        if (playerInventory)
        {
            if (player.CanPickUpItem && playerInventory.AddItem(item))
            {
                Destroy(gameObject);

                Debug.Log($"Player picked up \"{item.ItemName}\".");
            }
            else
            {
                onActionBlocked.Raise();
                return;
            }
        }
        else
            Debug.LogError("Can't pick up item because there is no reference to the player's inventory.");
    }
}
