using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Dash")]
public class BuyDash : ShopObject
{
    public override void BuyThisItem()
    {
        Main.Instance.player.GetComponent<Player>().UnlockDash();
    }
}
