using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Shop Item/Dash")]
public class BuyDash : ShopObject
{
    public override void BuyThisItem()
    {
        UnlockDash();
    }

    private void UnlockDash(){
        player.hasDash = true;
    }
}