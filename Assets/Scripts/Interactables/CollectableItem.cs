using UnityEngine;

public class CollectableItem : MonoBehaviour, IInteractableItem
{
    private const float RANGE = 2f;
    private const string INTERACTION_TEXT_ANALYSE = "Analyse";

    [SerializeField] private Item _item;

    [Header("Events")]
    [Tooltip("Event raised on analyse start")]
    [SerializeField] private ItemNexusEvent _onAnalyseEvent;

    [Tooltip("Event raised when player can't pick up item")]
    [SerializeField] private NexusEvent _onActionBlocked;

    private bool _hasBeenAnalysed;
    private Player _playerRef;

    public string InteractionText => _hasBeenAnalysed ? _item.ItemName : INTERACTION_TEXT_ANALYSE;

    public float Range => RANGE;

    public Item Item => _item;

    public bool HasBeenAnalysed
    {
        get => _hasBeenAnalysed;
        set
        {
            if (value)
            {
                _playerRef.IsAnalysingItem = false;
            }

            _hasBeenAnalysed = value;
        }
    }

    private void Awake()
    {
        if (!_item)
            Debug.LogError($"{name} doesn't have a item set to be picked up.");
    }

    public void Interact(Player player)
    {
        _playerRef = player;

        if (!_hasBeenAnalysed)
        {
            _onAnalyseEvent.Raise(this);

            player.IsAnalysingItem = true;

            return;
        }

        Storage playerInventory = player.Inventory;

        if (playerInventory)
        {
            if (player.CanPickUpItem && playerInventory.AddItem(_item))
            {
                Destroy(gameObject);

                Debug.Log($"Player picked up \"{_item.ItemName}\".");
            }
            else
            {
                _onActionBlocked.Raise();
                return;
            }
        }
        else
            Debug.LogError("Can't pick up item because there is no reference to the player's inventory.");
    }
}
