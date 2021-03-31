using UnityEngine;
using NaughtyAttributes;

public class Terminal : InteractableWithRequirements, ITerminal
{
    private const float RANGE = 3f;
    private const string INTERACTION_TEXT = "Terminal";

    [SerializeField] private TerminalData _terminalData;

    [Header("Events")]
    [Tooltip("Event raised when interactions begins")]
    [SerializeField] private NexusEvent<ITerminal> _onInteractionStartEvent;

    public float Range => RANGE;

    public string InteractionText => INTERACTION_TEXT;

    public TerminalData TerminalData => _terminalData;

    private void Awake()
    {
        if (!_terminalData)
        {
            Debug.LogError("Terminal doens't have terminal data.");

            return;
        }

        for (int i = 0; i < _terminalData.ControlSystems.Length; i++)
        {
            _terminalData.ControlSystems[i].Reset();
        }

        for (int i = 0; i < _terminalData.Users.Length; i++)
        {
            _terminalData.Users[i].Reset();
        }
    }

    public void Interact(Player player)
    {
        if (hasControlSystem)
        {
            if (controlSystem && !controlSystem.IsEngaged)
            {
                onActionBlocked.Raise();

                return;
            }
        }

        player.CurrentInteraction = this;

        _onInteractionStartEvent.Raise(this);
    }
}
