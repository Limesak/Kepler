using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Bomb launcher")]
public class BuyBombLauncher : ShopObject
{
    public override void BuyThisItem()
    {
        Main.Instance.player.GetComponent<Player>().UnlockBombs();
    }
}
