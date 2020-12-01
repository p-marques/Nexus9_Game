using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Variables/Interactable Variable")]
public class IInteractableVariable : ScriptableObject
{
    private IInteractable value;

    [SerializeField]
    [Tooltip("Event raised on value change.")]
    private NexusEvent<IInteractable> eventOnValueChanged;

    public IInteractable Value
    {
        get => value;

        set
        {
            if (this.value != value)
            {
                this.value = value;

                if (eventOnValueChanged) eventOnValueChanged.Raise(this.value);
            }
        }
    }
}
