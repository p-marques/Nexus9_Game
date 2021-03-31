using NaughtyAttributes;
using UnityEngine;

public class Toggle : InteractableWithRequirements, IInteractable
{
    private const float RANGE = 4f;
    private const string INTERACTION_TEXT = "Toggle";

    [ReadOnly]
    [SerializeField] private bool _isOn;

    private Renderer _renderer;

    public float Range => RANGE;

    public string InteractionText => INTERACTION_TEXT;

    public bool IsOn => _isOn;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        if (!_renderer) Debug.LogError("Failed getting the renderer.");
    }

    public void Interact(Player player)
    {
        if (!CheckControlSystem() || !CheckNecessaryItem(player.Inventory))
        {
            onActionBlocked.Raise();
            return;
        }

        _isOn = true;

        UpdateColor();
    }

    private void UpdateColor()
    {
        if (_isOn)
            _renderer.material.SetColor("_BaseColor", Color.green);
        else
            _renderer.material.SetColor("_BaseColor", Color.red);
    }
}
