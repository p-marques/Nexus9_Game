using UnityEngine;
using UnityEngine.Events;

public class NexusEventListener : MonoBehaviour
{
    [SerializeField] private NexusEvent _nexusEvent;

    [SerializeField]
    private UnityEvent _response;

    private void OnEnable()
    {
        _nexusEvent.Subscribe(this);
    }

    private void OnDisable()
    {
        _nexusEvent.Unsubscribe(this);
    }

    public void OnEventRaised()
    {
        _response.Invoke();
    }
}
