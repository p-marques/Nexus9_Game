using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Nexus Events/Simple Event", order = 1)]
public class NexusEvent : ScriptableObject
{
    private readonly LinkedList<NexusEventListener> listeners =
        new LinkedList<NexusEventListener>();

    public void Raise()
    {
        LinkedListNode<NexusEventListener> node;

        for (node = listeners.First; node != null; node = node.Next)
            node.Value.OnEventRaised();
    }

    public void Subscribe(NexusEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.AddLast(listener);
    }

    public void Unsubscribe(NexusEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
