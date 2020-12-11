using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private TextMeshProUGUI textField;

    [SerializeField]
    private TextMeshProUGUI itemNameField;

    [SerializeField]
    private Image fillBar;

    private bool isWorking;

    public void StartAnalyser(IInteractableItem interactableItem)
    {
        if (!isWorking)
        {
            isWorking = true;
            StartCoroutine(AnalyseCR(interactableItem));
        }
    }

    private IEnumerator AnalyseCR(IInteractableItem interactableItem)
    {
        string itemName = interactableItem.Item.ItemName;
        YieldInstruction wait = new WaitForSeconds(ANALYSING_TIME / ANALYSING_STEPS);
        float fillStepIncrease = 1f / ANALYSING_STEPS;
        
        textField.text = ANALYSING_TEXT;
        itemNameField.text = "";
        fillBar.fillAmount = 0f;

        panel.SetActive(true);

        for (int i = 0; i < ANALYSING_STEPS; i++)
        {
            textField.text += ".";

            fillBar.fillAmount += fillStepIncrease;

            yield return wait;
        }

        textField.text += DONE_TEXT;
        itemNameField.text = ID_TEXT;

        wait = new WaitForSeconds(DONE_EXTRA_TIME / itemName.Length);

        for (int i = 0; i < itemName.Length; i++)
        {
            itemNameField.text += itemName[i];

            yield return wait;
        }

        wait = new WaitForSeconds(ITEM_NAME_DISPLAY_TIME / ITEM_NAME_DISPLAY_STEPS);

        for (int i = 0; i < ITEM_NAME_DISPLAY_STEPS; i++)
        {
            itemNameField.alpha = itemNameField.alpha == 0f ? 1f : 0f;

            yield return wait;
        }

        panel.SetActive(false);

        isWorking = false;

        interactableItem.HasBeenAnalysed = true;
    }
}
