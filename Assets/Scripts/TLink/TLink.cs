public class TLink
{
    private const string WRONG_FORMAT = "Error: Wrong command format. Type \"help\" for clarification";
    private const string HELP = "help";
    private const string AUTH = "auth";
    private const string AUTH_SUCCESSFUL = "Authentication successful";
    private const string AUTH_UNSUCCESSFUL = "Error: wrong password";
    private const string AUTH_HELP = "Available commands:\n\tauth <userid> <password>";

    private TLinkState state;

    public TerminalData Data { get; set; }

    public TLink()
    {
        state = TLinkState.Auth;
    }

    public string HandleInput(string input)
    {
        string result;

        switch (state)
        {
            case TLinkState.Auth:
                result = HandleInputAuth(input.ToLower());
                break;
            case TLinkState.Normal:
            default:
                result = HandleInputNormal(input.ToLower());
                break;
        }

        return result;
    }

    private string HandleInputAuth(string input)
    {
        string[] values = input.Split();

        if (values[0] == AUTH)
        {
            if (values.Length != 3)
                return WRONG_FORMAT;

            TerminalUser user = GetUserById(values[1]);

            if (!user) return $"Error: no user found with id \"{values[1]}\"";

            bool wasUnlocked = user.Unlock(values[2]);

            if (wasUnlocked) return AUTH_SUCCESSFUL;
            else return AUTH_UNSUCCESSFUL;
        }
        else if (values[0] == HELP)
        {
            return AUTH_HELP;
        }

        return $"Command \"{values[0]}\" is unknown";
    }

    private string HandleInputNormal(string input)
    {
        return "Normal input handling not implemented";
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
}
