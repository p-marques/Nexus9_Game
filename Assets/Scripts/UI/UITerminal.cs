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
    [SerializeField] private GameObject _biosPostWrapper;

    [SerializeField] private GameObject _biosPostTerrellHeader;

    [SerializeField] private TextMeshProUGUI _biosPostMainField;

    [SerializeField] private TextMeshProUGUI _biosPostSecondaryField;

    [Header("Splash Screen")]
    [SerializeField] private GameObject _splashScreenWrapper;

    [SerializeField] private GameObject _tLinkVersionField;

    [Header("Console")]
    [SerializeField] private GameObject _consoleWrapper;

    [SerializeField] private TextMeshProUGUI _consoleOutput;

    [SerializeField] private GameObject _userInputWrapper;

    [SerializeField] private TextMeshProUGUI _userInputIdentifier;

    [SerializeField] private TMP_InputField _inputField;

    private ITerminal _currentTerminal;
    private TLink _tLink;
    private bool _isBusyWritting;
    private bool _isBusyProcessing;
    private WaitUntil _waitUntilNotBusyWritting;
    private WaitUntil _waitUntilNotBusyProcessing;
    private WaitForSeconds _waitBetweenPages;
    private WaitForSeconds _waitBiosInterval;

    private void Awake()
    {
        _waitUntilNotBusyWritting = new WaitUntil(() => _isBusyWritting == false);
        _waitUntilNotBusyProcessing = new WaitUntil(() => _isBusyProcessing == false);
        _waitBetweenPages = new WaitForSeconds(PAGE_TO_PAGE_INTERVAL);
        _waitBiosInterval = new WaitForSeconds(LINE_BY_LINE_INTERVAL);
        _tLink = new TLink();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _currentTerminal != null)
        {
            ShutDown();
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_inputField.text.Length > 0)
                ProcessInput();
        }
    }

    public void OnTerminalChange(ITerminal terminal)
    {
        _currentTerminal = terminal;
        _tLink.Data = terminal.TerminalData;

        StartCoroutine(BootUpTerminal());
    }

    private void ProcessInput()
    {
        string input = _inputField.text;

        DisableInput();

        _inputField.text = "";

        WriteLine(_consoleOutput, "> " + input + "\n", true);

        WriteLine(_consoleOutput, _tLink.HandleInput(input), false);

        EnableInput();
    }

    private IEnumerator BootUpTerminal()
    {
        _biosPostMainField.text = "";
        _biosPostSecondaryField.text = "";

        _biosPostWrapper.SetActive(true);

        yield return _waitBiosInterval;

        _biosPostTerrellHeader.SetActive(true);

        yield return _waitBiosInterval;

        StartCoroutine(WriteLineByLine(_biosPostMainField, _tLink.GetBiosPostMain(), LINE_BY_LINE_INTERVAL, true));

        yield return _waitUntilNotBusyWritting;

        _biosPostSecondaryField.text = _tLink.GetBiosPostSecondary();

        yield return _waitBetweenPages;

        _biosPostWrapper.SetActive(false);

        _splashScreenWrapper.SetActive(true);

        _tLinkVersionField.SetActive(true);

        yield return _waitBetweenPages;

        _splashScreenWrapper.SetActive(false);

        _consoleWrapper.SetActive(true);

        StartCoroutine(WriteLineByLine(_consoleOutput, _tLink.GetConsoleBoot(), LINE_BY_LINE_INTERVAL, true));

        yield return _waitUntilNotBusyWritting;

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
        _isBusyWritting = true;

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

                yield return _waitUntilNotBusyProcessing;
            }
            else
                field.text += node.Value;

            yield return wait;
        }

        _isBusyWritting = false;
    }

    private IEnumerator Processing(TextMeshProUGUI field, string finishedText)
    {
        _isBusyProcessing = true;

        WaitForSeconds wait = new WaitForSeconds(PROCESSING_TIME / PROCESSING_DOTS);

        for (int i = 0; i < PROCESSING_DOTS; i++)
        {
            field.text += ".";

            yield return wait;
        }

        field.text += finishedText;

        _isBusyProcessing = false;
    }

    private void EnableInput()
    {
        _userInputIdentifier.text = _tLink.ConsoleTypeIndicator;

        _userInputWrapper.SetActive(true);

        _inputField.Select();
        _inputField.ActivateInputField();
    }

    private void DisableInput()
    {
        _inputField.DeactivateInputField(true);

        _userInputWrapper.SetActive(false);
    }

    private void ShutDown()
    {
        StopAllCoroutines();

        _biosPostWrapper.SetActive(false);
        _splashScreenWrapper.SetActive(false);
        _consoleWrapper.SetActive(false);
        _tLinkVersionField.SetActive(false);
        _userInputWrapper.SetActive(false);
    }
}
