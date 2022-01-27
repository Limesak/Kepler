using System;
using UnityEngine;

namespace AsteroidBelt.Interfaces.Shop.Item_shop_behaviours
{
    [CreateAssetMenu(menuName = "Shop Item/Bomb launcher")]
    public class BuyBombLauncher : ShopObject
    {
        public override void BuyThisItem(){
            UnlockBombs();
        }

        private void UnlockBombs(){
            OnUnlockBombs?.Invoke();
        }

        public static event Action OnUnlockBombs;
    }
}