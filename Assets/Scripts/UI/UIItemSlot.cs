using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI itemName;

    [SerializeField]
    private TextMeshProUGUI metadata;

    public void UpdateInfo(Item inItem)
    {
        if (inItem == null)
        {
            icon.sprite = null;
            itemName.text = "";
            metadata.text = "";
        }
        else
        {
            icon.sprite = inItem.Icon;
            itemName.text = inItem.ItemName;
            metadata.text = inItem.Metadata;
        }
    }
}
