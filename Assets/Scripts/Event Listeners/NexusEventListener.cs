using UnityEngine;
using UnityEngine.Events;

public class NexusEventListener<T> : MonoBehaviour
{
    [SerializeField]
    private NexusEvent<T> nexusEvent;

    [SerializeField]
    private UnityEvent<T> response;

    private void OnEnable()
    {
        nexusEvent.Subscribe(this);
    }

    private void OnDisable()
    {
        nexusEvent.Unsubscribe(this);
    }

    public void OnEventRaised(T value)
    {
        response.Invoke(value);
    }
}
