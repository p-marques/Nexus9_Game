using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;

    [SerializeField] private TextMeshProUGUI _itemName;

    [SerializeField] private TextMeshProUGUI _metadata;

    public void UpdateInfo(Item inItem)
    {
        if (inItem == null)
        {
            _icon.sprite = null;
            _itemName.text = "";
            _metadata.text = "";
        }
        else
        {
            _icon.sprite = inItem.Icon;
            _itemName.text = inItem.ItemName;
            _metadata.text = inItem.Metadata;
        }
    }
}
