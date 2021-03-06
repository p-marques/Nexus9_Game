﻿using UnityEngine;
using UnityEngine.Events;

public class NexusEventListener : MonoBehaviour
{
    [SerializeField]
    private NexusEvent nexusEvent;

    [SerializeField]
    private UnityEvent response;

    private void OnEnable()
    {
        nexusEvent.Subscribe(this);
    }

    private void OnDisable()
    {
        nexusEvent.Unsubscribe(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}
