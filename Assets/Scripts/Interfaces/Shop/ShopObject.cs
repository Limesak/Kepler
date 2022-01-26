using UnityEngine;

public abstract class ShopObject : ScriptableObject
{
    [HideInInspector] public Player player;
    public string objectName;
    public int costInGold;
    public Sprite itemImage;

    public void Awake(){
        player = Main.Instance.player.GetComponent<Player>();
    }

    public abstract void BuyThisItem();
}
