using UnityEngine;

[CreateAssetMenu(menuName = "Game/Variables/Interactable Variable")]
public class IInteractableVariable : ScriptableObject
{
    private IInteractable _value;

    [Tooltip("Event raised on value change")]
    [SerializeField] private NexusEvent<IInteractable> _eventOnValueChanged;

    public IInteractable Value
    {
        get => _value;

        set
        {
            if (this._value != value)
            {
                this._value = value;

                if (_eventOnValueChanged) _eventOnValueChanged.Raise(this._value);
            }
        }
    }
}
