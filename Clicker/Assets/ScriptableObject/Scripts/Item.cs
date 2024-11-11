using UnityEngine;

public enum ItemType
{
    Gold
}
[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public ItemType Type;
    public int Value;
    public int Price;
}
