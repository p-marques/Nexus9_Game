using UnityEngine;
using UnityEngine.Events;

public class NexusEventListener<T> : MonoBehaviour
{
    [SerializeField] private NexusEvent<T> _nexusEvent;

    [SerializeField] private UnityEvent<T> _response;

    private void OnEnable()
    {
        _nexusEvent.Subscribe(this);
    }

    private void OnDisable()
    {
        _nexusEvent.Unsubscribe(this);
    }

    public void OnEventRaised(T value)
    {
        _response.Invoke(value);
    }
}
