using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Storage playerInventory;

    [SerializeField]
    private GameObject layoutGroup;

    [SerializeField]
    private GameObject itemUIPrefab;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            layoutGroup.SetActive(!layoutGroup.activeInHierarchy);
        }
    }
}
