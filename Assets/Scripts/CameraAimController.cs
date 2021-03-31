using UnityEngine;

public class CameraAimController : MonoBehaviour
{
    [Range(10, 300)]
    [SerializeField] private float _mouseSensitivity = 230f;

    [SerializeField] private float _maxLookUpRotation = 80f;

    [SerializeField] private float _maxLookDownRotation = 60f;

    private Player _playerRef;
    private float _inputMouseX;
    private float _inputMouseY;

    private void Awake()
    {
        _playerRef = GetComponent<Player>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!_playerRef.CurrentCamera) return;

        Transform camTransform = _playerRef.CurrentCamera.transform;

        HandleCameraPitch(camTransform);

        HandleParentRotation(camTransform);
    }

    private void Update()
    {
        _inputMouseX = Input.GetAxis("Mouse X");
        _inputMouseY = Input.GetAxis("Mouse Y");
    }

    private void HandleCameraPitch(Transform camTransform)
    {
        Vector3 cameraRotation = camTransform.localRotation.eulerAngles;
        
        cameraRotation.x -= _inputMouseY * _mouseSensitivity * Time.fixedDeltaTime;

        if (cameraRotation.x > 180f)
        {
            cameraRotation.x = Mathf.Max(cameraRotation.x, 360f - _maxLookUpRotation);
        }
        else
        {
            cameraRotation.x = Mathf.Min(cameraRotation.x, _maxLookDownRotation);
        }

        camTransform.localRotation = Quaternion.Euler(cameraRotation.x, 0f, 0f);
    }

    private void HandleParentRotation(Transform camTransform)
    {
        camTransform.parent.Rotate(Vector3.up * _inputMouseX * Time.fixedDeltaTime * _mouseSensitivity);
    }
}
