using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private string itemName = "new item";
    [SerializeField]
    private string metadata = "dummy txt";
    [SerializeField]
    private Sprite icon;


    public string ItemName => itemName;
    public string Metadata => metadata;
    public Sprite Icon => icon;
    
}
