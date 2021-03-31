using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Nexus Events/Simple Event", order = 1)]
public class NexusEvent : ScriptableObject
{
    private readonly LinkedList<NexusEventListener> _listeners =
        new LinkedList<NexusEventListener>();

    public void Raise()
    {
        LinkedListNode<NexusEventListener> node;

        for (node = _listeners.First; node != null; node = node.Next)
            node.Value.OnEventRaised();
    }

    public void Subscribe(NexusEventListener listener)
    {
        if (!_listeners.Contains(listener))
            _listeners.AddLast(listener);
    }

    public void Unsubscribe(NexusEventListener listener)
    {
        if (_listeners.Contains(listener))
            _listeners.Remove(listener);
    }
}
