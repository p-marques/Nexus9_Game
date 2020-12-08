using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Storage inventory;

    public Storage Inventory => inventory;

    private void Awake()
    {
        if (!inventory)
            Debug.LogError("Player doesn't have an inventory!");
    }
}
