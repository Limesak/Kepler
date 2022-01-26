using UnityEngine;

public abstract class ShopObject : ScriptableObject
{
    public string objectName;
    public int costInGold;
    public Sprite itemImage;

    public abstract void BuyThisItem();
}
