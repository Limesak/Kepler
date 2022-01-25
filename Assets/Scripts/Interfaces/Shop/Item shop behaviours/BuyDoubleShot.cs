using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/DoubleShot")]
public class BuyDoubleShot : ShopObject
{
    public override void BuyThisItem()
    {
        Main.Instance.player.GetComponent<Player>().UnlockDoubleShot();
    }
}
