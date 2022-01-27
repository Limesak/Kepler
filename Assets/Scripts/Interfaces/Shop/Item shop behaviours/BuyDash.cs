using System;
using UnityEngine;

namespace AsteroidBelt.Interfaces.Shop.Item_shop_behaviours
{
    [CreateAssetMenu(menuName = "Shop Item/Dash")]
    public class BuyDash : ShopObject
    {
        public override void BuyThisItem(){
            OnUnlockDash?.Invoke();
        }

        public static event Action OnUnlockDash;
    }
}