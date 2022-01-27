using System;
using UnityEngine;

namespace AsteroidBelt.Interfaces.Shop.Item_shop_behaviours
{
    [CreateAssetMenu(menuName = ("Shop Item/Bombs"))]
    public class BuyBombsPacks : ShopObject
    {
        public int bombsRegained;

        public override void BuyThisItem(){
            OnBombsBought(bombsRegained);
        }

        public static event Action<int> OnBombsBought;
    }
}
