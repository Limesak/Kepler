using UnityEngine;

namespace AsteroidBelt.Interfaces.Shop
{
    public abstract class ShopObject : ScriptableObject
    {
        public string objectName;
        public int costInGold;
        public Sprite itemImage;
        public bool needsBombLauncher;
        public bool buyOneAtATime;

        public abstract void BuyThisItem();
    }
}
