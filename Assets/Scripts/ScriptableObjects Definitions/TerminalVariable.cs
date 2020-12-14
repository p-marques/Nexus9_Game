using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Variables/Terminal Variable")]
public class TerminalVariable : ScriptableObject
{
    [SerializeField]
    [ReadOnly]
    private TerminalData value;

    [SerializeField]
    [Tooltip("Event raised on value change")]
    private NexusEvent onValueChanged;

    public TerminalData Value
    {
        get => value;
        set
        {
            this.value = value;

            if (onValueChanged) onValueChanged.Raise();
        }
    }
}
