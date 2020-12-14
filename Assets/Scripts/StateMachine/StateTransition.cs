using System;

public class StateTransition
{
    public Func<bool> Condition { get; }
    public IState Destination { get; }
    public Action Action { get; }

    public StateTransition(IState destination, Func<bool> condition, Action action = null)
    {
        Condition = condition;

        Destination = destination;

        Action = action;
    }
}
