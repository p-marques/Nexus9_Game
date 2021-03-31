using UnityEngine;
using NaughtyAttributes;

public class Door : InteractableWithRequirements, IInteractable
{
    private const float RANGE = 4f;
    private const string INTERACTION_TEXT = "Door";

    [Header("Toggle Requirement")]
    [SerializeField] private bool _needsToBeUnlocked;

    [ShowIf("needsToBeUnlocked")]
    [Tooltip("Toggle to lock/unlock door")]
    [SerializeField] private Toggle _toggle;

    private bool _isOpen;
    private Animator _animator;

    public string InteractionText => INTERACTION_TEXT;

    public float Range => RANGE;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact(Player player)
    {
        if (!CheckToggle() || !CheckNecessaryItem(player.Inventory) || !CheckControlSystem())
        {
            onActionBlocked.Raise();
            return;
        }

        _isOpen = !_isOpen;

        _animator.SetBool("IsOpen", _isOpen);
    }

    private bool CheckToggle()
    {
        if (_needsToBeUnlocked)
        {
            if (!_toggle)
            {
                Debug.LogError("Door needs toggle but no toggle is referenced.");
                return true;
            }

            return _toggle.IsOn;
        }

        return true;
    }
}
