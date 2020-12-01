using System.Collections.Generic;
using UnityEngine;

public class NexusEvent<T> : ScriptableObject
{
    private readonly LinkedList<NexusEventListener<T>> listeners = 
        new LinkedList<NexusEventListener<T>>();

    public void Raise(T raise)
    {
        LinkedListNode<NexusEventListener<T>> node;
        
        for (node = listeners.First; node != null; node = node.Next)
            node.Value.OnEventRaised(raise);
    }

    public void Subscribe(NexusEventListener<T> listener)
    {
        if (!listeners.Contains(listener))
            listeners.AddLast(listener);
    }

    public void Unsubscribe(NexusEventListener<T> listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
