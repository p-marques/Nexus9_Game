using TMPro;
using UnityEngine;

public class InteractionPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private TextMeshProUGUI interactionTextObject;

    public void UpdateInteractionPanel(IInteractable interactable)
    {
        if (interactable != null)
        {
            interactionTextObject.text = interactable.InteractionText;

            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
