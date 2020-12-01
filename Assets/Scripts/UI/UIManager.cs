using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject itemsPanel;

    [SerializeField]
    private GameObject itemUIPrefab;

    [SerializeField]
    private Storage storage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            itemsPanel.SetActive(!itemsPanel.activeInHierarchy);

            if (itemsPanel.activeInHierarchy)
            {
                UpdateInventory();
            }

        }
    }

    private void UpdateInventory()
    {
        for (int i = 0; i < itemsPanel.transform.childCount; i++)
        {
            Destroy(itemsPanel.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < storage.Items.Count; i++)
        {
            if(storage.Items[i] != null)
            {
                GameObject newItemUI = Instantiate(itemUIPrefab, itemsPanel.transform);

                newItemUI.GetComponent<ItemUI>().UpdateItemInfo(storage.Items[i]);
            }
        }
    }
}
