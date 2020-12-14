using TMPro;
using UnityEngine;

public class UIInteractionPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private TextMeshProUGUI interactionTextObject;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void UpdateInteractionPanel(IInteractable interactable)
    {
        if (interactable != null)
        {
            interactionTextObject.text = $"E) {interactable.InteractionText}";

            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
