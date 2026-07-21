using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public HeldItem itemType;
    public Sprite itemIcon;

}
