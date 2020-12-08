using UnityEngine;
using NaughtyAttributes;

public class Door : MonoBehaviour, IInteractable
{
    private const float RANGE = 4f;

    [SerializeField]
    private string interactionText;

    [SerializeField]
    private bool playerNeedsItem;

    [SerializeField]
    [ShowIf("playerNeedsItem")]
    [Tooltip("Item that the player needs to have to successfully interact")]
    private Item necessaryItem;

    public string InteractionText => interactionText;

    public float Range => RANGE;

    public void Interact(Player player)
    {
        if (playerNeedsItem)
        {
            if (!necessaryItem)
            {
                Debug.LogError("Item that the player needs to open door is not set!");
                return;
            }

            if (player.Inventory.HasItem(necessaryItem))
            {
                Debug.Log("Door opens...");
            }
            else
                Debug.Log("Can't open door because player doesn't have necessary item.");
        }
        else
            Debug.Log("Door opens...");
    }
}
