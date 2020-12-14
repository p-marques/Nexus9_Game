using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITerminal : MonoBehaviour
{
    private const byte PROCESSING_DOTS = 3;
    private const float PROCESSING_TIME = 2f;
    private const float WRITE_INTERVAL = 0.01f;
    private const string WELCOME = "Welcome to TERRELL Corporation (TM) TLink";
    private const string LOADING_AUTH = "Loading TERRELL (C) Authentication Tool v3.1";
    private const string FINISHED = "FINISHED";
    private const string GUEST_PROFILES = "Guest profiles";
    private const string NONE = "None";
    private const string PROFILES = "Profiles";
    private const string PROFILES_LIST = "profiles --list";

    [SerializeField]
    private TerminalVariable terminalVariable;

    [SerializeField]
    private TextMeshProUGUI terminalOutput;

    [SerializeField]
    private TextMeshProUGUI userId;

    [SerializeField]
    private TMP_InputField inputField;

    private TLink tLink;
    private bool isBusy;
    private WaitUntil waitUntilNotBusy;

    private void Awake()
    {
        waitUntilNotBusy = new WaitUntil(() => isBusy == false);
        tLink = new TLink();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inputField.text.Length > 0)
                StartCoroutine(ProcessInput());
        }
    }

    public void OnTerminalChange()
    {
        tLink.Data = terminalVariable.Value;

        StartCoroutine(BootUpTerminal());
    }

    private IEnumerator ProcessInput()
    {
        DisableInput();

        string input = inputField.text;

        inputField.text = "";

        NewLines();

        StartCoroutine(LazyWrite("> " + input));

        yield return waitUntilNotBusy;

        

        EnableInput();
    }

    private IEnumerator BootUpTerminal()
    {
        ClearOutput();

        StartCoroutine(LazyWrite(WELCOME));

        yield return waitUntilNotBusy;

        NewLines();

        StartCoroutine(LazyWrite(LOADING_AUTH));

        yield return waitUntilNotBusy;

        StartCoroutine(Processing(FINISHED));

        yield return waitUntilNotBusy;

        NewLines();

        StartCoroutine(LazyWrite(GUEST_PROFILES));

        yield return waitUntilNotBusy;

        StartCoroutine(Processing(NONE));

        yield return waitUntilNotBusy;

        NewLines();

        StartCoroutine(LazyWrite(PROFILES));

        yield return waitUntilNotBusy;

        StartCoroutine(Processing(terminalVariable.Value.Users.Length.ToString()));

        yield return waitUntilNotBusy;

        NewLines();

        StartCoroutine(LazyWrite(PROFILES_LIST));

        yield return waitUntilNotBusy;

        NewLines();

        for (int i = 0; i < terminalVariable.Value.Users.Length; i++)
        {
            StartCoroutine(LazyWrite(terminalVariable.Value.Users[i].ToString()));

            yield return waitUntilNotBusy;

            NewLines(1);
        }

        EnableInput();
    }

    private IEnumerator LazyWrite(string value)
    {
        isBusy = true;

        WaitForSeconds wait = new WaitForSeconds(WRITE_INTERVAL);

        for (int i = 0; i < value.Length; i++)
        {
            terminalOutput.text += value[i];

            yield return wait;
        }

        isBusy = false;
    }

    private IEnumerator Processing(string finishedText)
    {
        isBusy = true;

        WaitForSeconds wait = new WaitForSeconds(PROCESSING_TIME / PROCESSING_DOTS);

        for (int i = 0; i < PROCESSING_DOTS; i++)
        {
            terminalOutput.text += ".";

            yield return wait;
        }

        terminalOutput.text += finishedText;

        isBusy = false;
    }

    private void NewLines(int numberOfLines = 2)
    {
        terminalOutput.text += new string('\n', numberOfLines);
    }

    private void ClearOutput()
    {
        terminalOutput.text = "";
    }

    private void EnableInput()
    {
        inputField.ActivateInputField();
    }

    private void DisableInput()
    {
        inputField.DeactivateInputField(true);
    }
}
