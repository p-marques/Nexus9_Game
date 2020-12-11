using NaughtyAttributes;
using UnityEngine;

public class Toggle : MonoBehaviour, IInteractable
{
    private const float RANGE = 4f;
    private const string INTERACTION_TEXT = "Toggle";

    [SerializeField]
    private bool playerNeedsItem;

    [SerializeField]
    [ShowIf("playerNeedsItem")]
    [Tooltip("Item that the player needs to have to successfully interact")]
    private Item necessaryItem;

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
        if (playerNeedsItem)
        {
            if (!necessaryItem)
            {
                Debug.LogError("Player needs item but no item ref is set.");
                return;
            }

            if (player.Inventory.HasItem(necessaryItem))
            {
                isOn = !isOn;
            }
        }
        else
            isOn = !isOn;

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
