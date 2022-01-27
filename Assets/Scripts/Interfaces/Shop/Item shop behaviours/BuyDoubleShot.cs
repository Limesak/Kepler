using System;
using UnityEngine;

namespace AsteroidBelt.Interfaces.Shop.Item_shop_behaviours
{
    [CreateAssetMenu(menuName = "Shop Item/DoubleShot")]
    public class BuyDoubleShot : ShopObject
    {
        public override void BuyThisItem(){
            OnUnlockDoubleShot?.Invoke();
        }

        public static event Action OnUnlockDoubleShot;
    }
}
