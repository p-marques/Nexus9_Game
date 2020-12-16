using NaughtyAttributes;
using UnityEngine;

public class InteractableWithRequirements : MonoBehaviour
{
    [Header("Requirements to interact")]
    [SerializeField]
    protected bool playerNeedsItem;

    [SerializeField]
    [ShowIf("playerNeedsItem")]
    [Tooltip("Item that the player needs to have to successfully interact")]
    protected Item necessaryItem;

    [SerializeField]
    protected bool hasControlSystem;

    [SerializeField]
    [ShowIf("hasControlSystem")]
    [Tooltip("Control system that needs to be engaged before interaction")]
    protected TerminalCS controlSystem;

    [Header("Event raised when requirements not met")]
    [Tooltip("Event raised when player can't interact with door")]
    [SerializeField]
    protected NexusEvent onActionBlocked;

    protected bool CheckNecessaryItem(Storage inventory)
    {
        if (playerNeedsItem)
        {
            if (!necessaryItem)
            {
                Debug.LogError("Item that the player needs to open door is not set!");
                return true;
            }

            return inventory.HasItem(necessaryItem);
        }

        return true;
    }

    protected bool CheckControlSystem()
    {
        if (hasControlSystem)
        {
            if (!controlSystem)
            {
                Debug.LogError("Control system needed is not set!");
                return true;
            }

            return controlSystem.IsEngaged;
        }

        return true;
    }
}
