using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : IState
{
    private Dictionary<Type, List<StateTransition>> transitions;

    public T CurrentState { get; private set; }

    public StateMachine()
    {
        transitions = new Dictionary<Type, List<StateTransition>>();
    }

    public void PhysicsTick()
    {
        CurrentState?.PhysicsTick();
    }

    public void Tick()
    {
        StateTransition transition = GetStateTransition();

        if (transition != null)
        {
            transition.Action?.Invoke();

            SetState(transition.Destination);
        }

        CurrentState?.Tick();
    }

    public void SetState(IState state)
    {
        if (state == (CurrentState as IState))
            return;

        Debug.Log($"State transition: {CurrentState?.GetType().Name} --> {state.GetType().Name}");

        CurrentState?.OnExit();
        CurrentState = (T)state;

        CurrentState.OnEnter();
    }

    public void AddTransition(IState origin, IState destination, Func<bool> condition, Action action = null)
    {
        Type originType = origin.GetType();
        bool isListed = transitions
            .TryGetValue(originType, out List<StateTransition> stateTransitions);

        if (!isListed)
        {
            stateTransitions = new List<StateTransition>();
            transitions[originType] = stateTransitions;
        }

        stateTransitions.Add(new StateTransition(destination, condition, action));
    }

    private StateTransition GetStateTransition()
    {
        Type stateType = CurrentState.GetType();

        if (transitions.ContainsKey(stateType))
        {
            List<StateTransition> availableTransitions = transitions[stateType];

            for (int i = 0; i < availableTransitions.Count; i++)
            {
                StateTransition transition = availableTransitions[i];

                if (transition.Condition())
                    return transition;
            }
        }

        return null;
    }
}
