using UnityEngine;
using NaughtyAttributes;

public class Door : InteractableWithRequirements, IInteractable
{
    private const float RANGE = 4f;
    private const string INTERACTION_TEXT = "Door";

    [Header("Toggle Requirement")]
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
        if (!CheckToggle() || !CheckNecessaryItem(player.Inventory) || !CheckControlSystem())
        {
            onActionBlocked.Raise();
            return;
        }

        isOpen = !isOpen;

        animator.SetBool("IsOpen", isOpen);
    }

    private bool CheckToggle()
    {
        if (needsToBeUnlocked)
        {
            if (!toggle)
            {
                Debug.LogError("Door needs toggle but no toggle is referenced.");
                return true;
            }

            return toggle.IsOn;
        }

        return true;
    }
}
