using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Shop Item/Bombs"))]
public class BuyBombsPacks : ShopObject
{
    public int thisBombsPack;

    public override void BuyThisItem()
    {
        Main.Instance.player.GetComponent<Player>().PickUpBombs(thisBombsPack);
    }
}
