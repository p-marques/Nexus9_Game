using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Game/Storage/New Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string _itemName = "new item";
    [SerializeField] private string _metadata = "dummy txt";
    [SerializeField] private Sprite _icon;


    public string ItemName => _itemName;
    public string Metadata => _metadata;
    public Sprite Icon => _icon;
    
}
