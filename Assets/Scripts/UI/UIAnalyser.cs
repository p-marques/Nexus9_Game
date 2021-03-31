using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAnalyser : MonoBehaviour
{
    private const string ANALYSING_TEXT = "Analysing";
    private const float ANALYSING_TIME = 5f;
    private const byte ANALYSING_STEPS = 3;
    private const string DONE_TEXT = "Done";
    private const float DONE_EXTRA_TIME = 2f;
    private const string ID_TEXT = "ID: ";
    private const float ITEM_NAME_DISPLAY_TIME = 2f;
    private const byte ITEM_NAME_DISPLAY_STEPS = 4;

    [SerializeField] private GameObject _panel;

    [SerializeField] private TextMeshProUGUI _textField;

    [SerializeField] private TextMeshProUGUI _itemNameField;

    [SerializeField] private Image _fillBar;

    private bool _isWorking;

    public void StartAnalyser(IInteractableItem interactableItem)
    {
        if (!_isWorking)
        {
            _isWorking = true;
            StartCoroutine(AnalyseCR(interactableItem));
        }
    }

    private IEnumerator AnalyseCR(IInteractableItem interactableItem)
    {
        string itemName = interactableItem.Item.ItemName;
        YieldInstruction wait = new WaitForSeconds(ANALYSING_TIME / ANALYSING_STEPS);
        float fillStepIncrease = 1f / ANALYSING_STEPS;
        
        _textField.text = ANALYSING_TEXT;
        _itemNameField.text = "";
        _fillBar.fillAmount = 0f;

        _panel.SetActive(true);

        for (int i = 0; i < ANALYSING_STEPS; i++)
        {
            _textField.text += ".";

            _fillBar.fillAmount += fillStepIncrease;

            yield return wait;
        }

        _textField.text += DONE_TEXT;
        _itemNameField.text = ID_TEXT;

        wait = new WaitForSeconds(DONE_EXTRA_TIME / itemName.Length);

        for (int i = 0; i < itemName.Length; i++)
        {
            _itemNameField.text += itemName[i];

            yield return wait;
        }

        wait = new WaitForSeconds(ITEM_NAME_DISPLAY_TIME / ITEM_NAME_DISPLAY_STEPS);

        for (int i = 0; i < ITEM_NAME_DISPLAY_STEPS; i++)
        {
            _itemNameField.alpha = _itemNameField.alpha == 0f ? 1f : 0f;

            yield return wait;
        }

        _panel.SetActive(false);

        _isWorking = false;

        interactableItem.HasBeenAnalysed = true;
    }
}
