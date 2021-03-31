using TMPro;
using UnityEngine;

public class UIInteractionPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private TextMeshProUGUI _interactionTextObject;

    private void Start()
    {
        _panel.SetActive(false);
    }

    public void UpdateInteractionPanel(IInteractable interactable)
    {
        if (interactable != null)
        {
            _interactionTextObject.text = $"E) {interactable.InteractionText}";

            _panel.SetActive(true);
        }
        else
        {
            _panel.SetActive(false);
        }
    }
}
