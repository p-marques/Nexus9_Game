using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Storage inventory;

    private PlayerController controller;
    private CameraAimController cameraAim;

    public Storage Inventory => inventory;

    private void Awake()
    {
        controller = GetComponentInChildren<PlayerController>();
        cameraAim = GetComponentInChildren<CameraAimController>();

        if (!inventory)
            Debug.LogError("Player doesn't have an inventory!");

        controller.CanMove = true;
        cameraAim.CanMove = true;
    }

    public void SetCanMove(bool value)
    {
        controller.CanMove = value;
        cameraAim.CanMove = value;
    }
}
