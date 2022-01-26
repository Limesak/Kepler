using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Shop Item/DoubleShot")]
public class BuyDoubleShot : ShopObject
{
    public override void BuyThisItem()
    {
        UnlockDoubleShot();
    }

    private void UnlockDoubleShot(){
        player.hasDoubleShot = true;
    }
}
