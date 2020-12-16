using NaughtyAttributes;
using UnityEngine;

public class Toggle : InteractableWithRequirements, IInteractable
{
    private const float RANGE = 4f;
    private const string INTERACTION_TEXT = "Toggle";

    [SerializeField]
    [ReadOnly]
    private bool isOn;

    private new Renderer renderer;

    public float Range => RANGE;

    public string InteractionText => INTERACTION_TEXT;

    public bool IsOn => isOn;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();

        if (!renderer) Debug.LogError("Failed getting the renderer.");
    }

    public void Interact(Player player)
    {
        if (!CheckControlSystem() || !CheckNecessaryItem(player.Inventory))
        {
            onActionBlocked.Raise();
            return;
        }

        isOn = true;

        UpdateColor();
    }

    private void UpdateColor()
    {
        if (isOn)
            renderer.material.SetColor("_BaseColor", Color.green);
        else
            renderer.material.SetColor("_BaseColor", Color.red);
    }
}
