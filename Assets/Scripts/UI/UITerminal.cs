using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITerminal : MonoBehaviour
{
    private const byte PROCESSING_DOTS = 3;
    private const float PROCESSING_TIME = 0.25f;
    private const float PAGE_TO_PAGE_INTERVAL = 1.5f;
    private const float LINE_BY_LINE_INTERVAL = 0.5f;

    [Header("Bios Post")]
    [SerializeField]
    private GameObject biosPostWrapper;

    [SerializeField]
    private GameObject biosPostTerrellHeader;

    [SerializeField]
    private TextMeshProUGUI biosPostMainField;

    [SerializeField]
    private TextMeshProUGUI biosPostSecondaryField;

    [Header("Splash Screen")]
    [SerializeField]
    private GameObject splashScreenWrapper;

    [SerializeField]
    private GameObject tLinkVersionField;

    [Header("Console")]
    [SerializeField]
    private GameObject consoleWrapper;

    [SerializeField]
    private TextMeshProUGUI consoleOutput;

    [SerializeField]
    private GameObject userInputWrapper;

    [SerializeField]
    private TextMeshProUGUI userInputIdentifier;

    [SerializeField]
    private TMP_InputField inputField;

    private ITerminal currentTerminal;
    private TLink tLink;
    private bool isBusyWritting;
    private bool isBusyProcessing;
    private WaitUntil waitUntilNotBusyWritting;
    private WaitUntil waitUntilNotBusyProcessing;
    private WaitForSeconds waitBetweenPages;
    private WaitForSeconds waitBiosInterval;

    private void Awake()
    {
        waitUntilNotBusyWritting = new WaitUntil(() => isBusyWritting == false);
        waitUntilNotBusyProcessing = new WaitUntil(() => isBusyProcessing == false);
        waitBetweenPages = new WaitForSeconds(PAGE_TO_PAGE_INTERVAL);
        waitBiosInterval = new WaitForSeconds(LINE_BY_LINE_INTERVAL);
        tLink = new TLink();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentTerminal != null)
        {
            ShutDown();
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inputField.text.Length > 0)
                ProcessInput();
        }
    }

    public void OnTerminalChange(ITerminal terminal)
    {
        currentTerminal = terminal;
        tLink.Data = terminal.TerminalData;

        StartCoroutine(BootUpTerminal());
    }

    private void ProcessInput()
    {
        string input = inputField.text;

        DisableInput();

        inputField.text = "";

        WriteLine(consoleOutput, "> " + input + "\n", true);

        WriteLine(consoleOutput, tLink.HandleInput(input), false);

        EnableInput();
    }

    private IEnumerator BootUpTerminal()
    {
        biosPostMainField.text = "";
        biosPostSecondaryField.text = "";

        biosPostWrapper.SetActive(true);

        yield return waitBiosInterval;

        biosPostTerrellHeader.SetActive(true);

        yield return waitBiosInterval;

        StartCoroutine(WriteLineByLine(biosPostMainField, tLink.GetBiosPostMain(), LINE_BY_LINE_INTERVAL, true));

        yield return waitUntilNotBusyWritting;

        biosPostSecondaryField.text = tLink.GetBiosPostSecondary();

        yield return waitBetweenPages;

        biosPostWrapper.SetActive(false);

        splashScreenWrapper.SetActive(true);

        tLinkVersionField.SetActive(true);

        yield return waitBetweenPages;

        splashScreenWrapper.SetActive(false);

        consoleWrapper.SetActive(true);

        StartCoroutine(WriteLineByLine(consoleOutput, tLink.GetConsoleBoot(), LINE_BY_LINE_INTERVAL, true));

        yield return waitUntilNotBusyWritting;

        EnableInput();
    }

    private void WriteLine(TextMeshProUGUI field, string value, bool clearField)
    {
        if (clearField)
            field.text = "";

        field.text += value + "\n";
    }

    private IEnumerator WriteLineByLine(TextMeshProUGUI field, 
        LinkedList<string> values, float interval, bool clearField)
    {
        isBusyWritting = true;

        WaitForSeconds wait = new WaitForSeconds(interval);
        LinkedListNode<string> node;

        if (clearField)
            field.text = "";

        for (node = values.First; node != null; node = node.Next)
        {
            if (node.Value.Contains("#"))
            {
                string[] split = node.Value.Split('#');

                field.text += split[0];

                StartCoroutine(Processing(field, split[1]));

                yield return waitUntilNotBusyProcessing;
            }
            else
                field.text += node.Value;

            yield return wait;
        }

        isBusyWritting = false;
    }

    private IEnumerator Processing(TextMeshProUGUI field, string finishedText)
    {
        isBusyProcessing = true;

        WaitForSeconds wait = new WaitForSeconds(PROCESSING_TIME / PROCESSING_DOTS);

        for (int i = 0; i < PROCESSING_DOTS; i++)
        {
            field.text += ".";

            yield return wait;
        }

        field.text += finishedText;

        isBusyProcessing = false;
    }

    private void EnableInput()
    {
        userInputIdentifier.text = tLink.ConsoleTypeIndicator;

        userInputWrapper.SetActive(true);

        inputField.ActivateInputField();
    }

    private void DisableInput()
    {
        inputField.DeactivateInputField(true);

        userInputWrapper.SetActive(false);
    }

    private void ShutDown()
    {
        StopAllCoroutines();

        biosPostWrapper.SetActive(false);
        splashScreenWrapper.SetActive(false);
        consoleWrapper.SetActive(false);
        tLinkVersionField.SetActive(false);
    }
}
