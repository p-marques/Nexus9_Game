using System.Collections.Generic;

public class TLink
{
    private const string WRONG_FORMAT = "Error: Wrong command format. Type \"help\" for clarification";
    private const string HELP = "help";
    private const string AUTH = "auth";
    private const string LOAD = "load";
    private const string COMMS = "comms";
    private const string CONTROL_SYSTEMS = "cs";
    private const string RETURN = "return"; 
    private const string AUTH_SUCCESSFUL = "Authentication successful";
    private const string AUTH_UNSUCCESSFUL = "Error: wrong password";
    private const string AUTH_HELP = "Available commands:\n\tauth <userid> <password>";
    private const string LOGGED_IN_HELP = "Available commands:\n\tload comms | loads your communications\n\tload cs | loads control systems";
    private const string COMMS_HELP = "Available commands:\n\tlist | lists all communications\n\topen <index> | opens specific communication";
    private const string CS_HELP = "Available commands:\n\tlist | lists all control systems\n\tengage <index> | engages specific control system";
    private const string WELCOME = "Welcome to TERRELL Corporation (TM) TLink\n\n";
    private const string LOADING_AUTH = "Loading TERRELL (C) Authentication Tool v3.1#Finished\n\n";
    private const string GUEST_PROFILES = "Guest profiles#None\n";
    private const string PROFILES = "Profiles#";
    private const string PROFILES_LIST = "profiles --list\n\n";
    private const byte COMM_SUBJECT_LENGTH = 50;

    private TLinkState state;
    private TerminalUser currentUser;
    private TerminalData data;

    public string ConsoleTypeIndicator
    {
        get
        {
            if (state == TLinkState.LoggedIn)
                return currentUser.Id + " >";

            return "input >";
        }
    }

    public TerminalData Data 
    {
        get => data;
        set
        {
            data = value;

            AttemptAutomaticLogin();
        }
    }

    public TLink()
    {
        state = TLinkState.Auth;
    }

    public string HandleInput(string input)
    {
        string result = "";

        input = input.Trim();

        switch (state)
        {
            case TLinkState.Auth:
                result = HandleInputAuth(input);
                break;
            case TLinkState.LoggedIn:
                result = HandleInputLoggedIn(input);
                break;
            case TLinkState.COMMS:
                result = HandleInputCOMMS(input);
                break;
            case TLinkState.ControlSystems:
                result = HandleInputCS(input);
                break;
        }

        return result;
    }

    public LinkedList<string> GetBiosPostMain()
    {
        LinkedList<string> values = new LinkedList<string>();

        values.AddLast("TERRELL BIOS (C) 2016 Terrell Corporation\n");

        values.AddLast("TERRELL P5KPL ACPI BIOS Revision 0901\n");

        values.AddLast("CPU : TERRELL(TM) SYTIR(TM) Octa CPU L3090KX @ 5.00GHz\n");

        values.AddLast("\tSpeed : 5.80GHz\tCount : 8\n\n\n");

        values.AddLast("DDR6-7200 in Eight-Channel Mode\n");

        values.AddLast("262144MB#OK\n\n");

        values.AddLast("TSB Device(s) : 1 Keyboard, 2 Storage Device(s)\n\n");

        values.AddLast("Auto-Detecting Pri Main#U.4 SSD TZ-55AB5491\n");

        values.AddLast("Auto-Detecting Pri Secondary#U.4 SSD TZ-55AC1433\n");

        return values;
    }

    public string GetBiosPostSecondary()
    {
        string value;

        value = "(TM) Terrell Corporation\n";
        value += "12102016-0901-00101111-049808-Capelake-CG820000-Y2KC";

        return value;
    }

    public LinkedList<string> GetConsoleBoot()
    {
        LinkedList<string> values = new LinkedList<string>();

        values.AddLast(WELCOME);

        if (state == TLinkState.Auth)
        {
            values.AddLast(LOADING_AUTH);

            values.AddLast(GUEST_PROFILES);

            values.AddLast(PROFILES + data.Users.Length + "\n\n");

            values.AddLast(PROFILES_LIST);

            for (int i = 0; i < data.Users.Length; i++)
            {
                string t = data.Users[i].ToString() + "\n";

                if (i + 1 == data.Users.Length)
                    t += "\n";

                values.AddLast(t);
            }
        }

        return values;
    }

    private string HandleInputAuth(string input)
    {
        string[] values = input.Split();

        if (values[0].Trim().ToLower() == AUTH)
        {
            if (values.Length != 3)
                return WRONG_FORMAT;

            TerminalUser user = GetUserById(values[1].Trim().ToLower());

            if (!user)
                return $"Error: no user found with id \"{values[1]}\"";

            bool wasUnlocked = user.Unlock(values[2]);

            if (wasUnlocked)
            {
                state = TLinkState.LoggedIn;
                currentUser = user;

                return AUTH_SUCCESSFUL;
            }
            else
                return AUTH_UNSUCCESSFUL;
        }
        else if (values[0].Trim() == HELP)
        {
            return AUTH_HELP;
        }

        return $"Command \"{values[0]}\" is unknown";
    }

    private string HandleInputLoggedIn(string input)
    {
        string[] values = input.ToLower().Split();

        if (values[0].Trim() == LOAD)
        {
            if (values.Length != 2)
                return WRONG_FORMAT;

            string program = values[1].Trim();

            if (program == COMMS)
            {
                state = TLinkState.COMMS;

                return "LOADED : TERRELL (C) Comms Inbox v1.14\n\n" + GetCommsInbox();
            }
            else if (program == CONTROL_SYSTEMS)
            {
                state = TLinkState.ControlSystems;

                return "LOADED : TERRELL (C) cs.NET v4.04\n\n" + GetControlSystemsList();
            }
        }
        else if (values[0].Trim() == HELP)
        {
            return LOGGED_IN_HELP;
        }

        return $"Command \"{values[0]}\" is unknown";
    }

    private string HandleInputCOMMS(string input)
    {
        string[] values = input.ToLower().Split();
        string identifier = values[0].Trim();


        if (identifier == "list")
        {
            if (values.Length != 1)
                return WRONG_FORMAT;

            return GetCommsInbox();
        }
        else if (identifier == "open")
        {
            if (values.Length != 2)
                return WRONG_FORMAT;

            string indexGiven = values[1].Trim();

            if (int.TryParse(indexGiven, out int result))
            {
                if (result < 0 || result >= currentUser.Communications.Length)
                {
                    return $"No communication has an index of \"{indexGiven}\"";
                }

                return GetCommunication(result);
            }
            else
                return $"Index \"{indexGiven}\" is not valid";
        }
        else if (identifier == RETURN)
        {
            state = TLinkState.LoggedIn;

            return "Returned to main";
        }
        else if (identifier == HELP)
        {
            return COMMS_HELP;
        }

        return $"Command \"{identifier}\" is unknown";
    }

    private string HandleInputCS(string input)
    {
        string[] values = input.ToLower().Split();
        string identifier = values[0].Trim();


        if (identifier == "list")
        {
            if (values.Length != 1)
                return WRONG_FORMAT;

            return GetControlSystemsList();
        }
        else if (identifier == "engage")
        {
            if (values.Length != 2)
                return WRONG_FORMAT;

            string indexGiven = values[1].Trim();

            if (int.TryParse(indexGiven, out int result))
            {
                TerminalCS cs = GetControlSystemByIndex(result);

                if (cs == null)
                {
                    return $"Index \"{indexGiven}\" is not valid";
                }
                else
                {
                    cs.IsEngaged = true;

                    return $"Control system \"{cs.Name}\" engaged successfully";
                }
            }
            else
                return $"Index \"{indexGiven}\" is not valid";
        }
        else if (identifier == RETURN)
        {
            state = TLinkState.LoggedIn;

            return "Returned to main";
        }
        else if (identifier == HELP)
        {
            return CS_HELP;
        }

        return $"Command \"{identifier}\" is unknown";
    }

    private string GetCommsInbox()
    {
        string value = "";

        value += "== INBOX ==\n";

        for (int i = 0; i < currentUser.Communications.Length; i++)
        {
            TerminalCommunication comm = currentUser.Communications[i];

            value += $"{i}: {comm.Text.Substring(0, COMM_SUBJECT_LENGTH)} | From: {comm.From}\n";
        }

        return value;
    }

    private string GetCommunication(int index)
    {
        string value = "";
        TerminalCommunication comm = currentUser.Communications[index];

        value += "== METADATA ==\n";
        
        value += $"From: {comm.From}\n\n";

        value += "== BODY ==\n";

        value += comm.Text + "\n\n";

        value += "== END OF COMMMUNICATION ==";

        return value;
    }

    private string GetControlSystemsList()
    {
        string value;

        value = "== CONTROL SYSTEMS ==\n";

        for (int i = 0; i < data.ControlSystems.Length; i++)
        {
            TerminalCS cs = data.ControlSystems[i];

            value += $"{i}: {cs.Name}\n";
        }

        return value;
    }

    private TerminalUser GetUserById(string id)
    {
        for (int i = 0; i < Data.Users.Length; i++)
        {
            TerminalUser user = Data.Users[i];

            if (user.Id == id)
                return user;
        }

        return null;
    }

    private TerminalCS GetControlSystemByIndex(int index)
    {
        TerminalCS cs = null;

        if (index >= 0 && index < data.ControlSystems.Length)
        {
            cs = data.ControlSystems[index];
        }

        return cs;
    }

    private void AttemptAutomaticLogin()
    {
        for (int i = 0; i < data.Users.Length; i++)
        {
            TerminalUser user = data.Users[i];

            if (user.IsUnlocked)
            {
                state = TLinkState.LoggedIn;
                currentUser = user;

                return;
            }
        }

        state = TLinkState.Auth;
        currentUser = null;
    }
}
