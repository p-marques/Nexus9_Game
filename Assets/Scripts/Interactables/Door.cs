using UnityEngine;
using NaughtyAttributes;

public class Door : MonoBehaviour, IInteractable
{
    private const float RANGE = 4f;
    private const string INTERACTION_TEXT = "Door";

    [Header("Requirements to interact")]
    [SerializeField]
    private bool playerNeedsItem;

    [SerializeField]
    [ShowIf("playerNeedsItem")]
    [Tooltip("Item that the player needs to have to successfully interact")]
    private Item necessaryItem;

    [SerializeField]
    private bool needsToBeUnlocked;

    [SerializeField]
    [ShowIf("needsToBeUnlocked")]
    [Tooltip("Toggle to lock/unlock door")]
    private Toggle toggle;

    private bool isOpen;
    private Animator animator;

    public string InteractionText => INTERACTION_TEXT;

    public float Range => RANGE;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact(Player player)
    {
        bool isOn, playerHasItem;

        isOn = CheckToggle();

        playerHasItem = CheckNecessaryItem(player.Inventory);

        if (!isOn) Debug.Log("Door is locked.");

        if (!playerHasItem) Debug.Log("Player needs item to open door.");

        if (isOn && playerHasItem)
        {
            isOpen = !isOpen;

            animator.SetBool("IsOpen", isOpen);
        }
    }

    private bool CheckToggle()
    {
        if (needsToBeUnlocked)
        {
            if (!toggle)
            {
                Debug.LogError("Door needs toggle but no toggle is referenced.");
                return false;
            }

            return toggle.IsOn;
        }

        return true;
    }

    private bool CheckNecessaryItem(Storage inventory)
    {
        if (playerNeedsItem)
        {
            if (!necessaryItem)
            {
                Debug.LogError("Item that the player needs to open door is not set!");
                return false;
            }

            return inventory.HasItem(necessaryItem);
        }

        return true;
    }
}
