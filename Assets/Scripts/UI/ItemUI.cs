using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI itemName;

    [SerializeField]
    private TextMeshProUGUI metadata;

    public void UpdateItemInfo(Item inItem)
    {
        icon.sprite = inItem.Icon;
        itemName.text = inItem.ItemName;
        metadata.text = inItem.Metadata;
    }
}
