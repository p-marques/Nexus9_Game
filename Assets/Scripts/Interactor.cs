using UnityEngine;

public class Interactor : MonoBehaviour
{
    private const float RAYCAST_DISTANCE = 30f;

    [SerializeField] private IInteractableVariable _currentInteractable;
    
    private Player _playerRef;

    private void Awake()
    {
        _playerRef = GetComponent<Player>();

        if (!_playerRef)
            Debug.LogError("Interactor failed to find component Player!");
    }

    private void Update()
    {
        if (!_playerRef.CurrentCamera)
        {
            _currentInteractable.Value = null;

            return;
        }

        Transform cameraTransform = _playerRef.CurrentCamera.transform;
        IInteractable hitInteractable = null;

        bool hasHit = Physics.Raycast(cameraTransform.position, cameraTransform.forward, 
            out RaycastHit hitInfo, RAYCAST_DISTANCE);

        if (hasHit)
        {
            hitInteractable = hitInfo.transform.GetComponent<IInteractable>();

            if (hitInteractable != null && hitInfo.distance > hitInteractable.Range)
            {
                hitInteractable = null;
            }
        }

        _currentInteractable.Value = hitInteractable;

        if (_currentInteractable.Value != null && Input.GetKeyDown(KeyCode.E))
        {
            _currentInteractable.Value.Interact(_playerRef);
        }
    }
}
