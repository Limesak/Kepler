using UnityEngine;
using System;

[CreateAssetMenu(menuName = ("Shop Item/Bombs"))]
public class BuyBombsPacks : ShopObject
{
    public int bombsRegained;

    public override void BuyThisItem(){
        OnBombsBought(bombsRegained);
    }

    public static event Action<int> OnBombsBought;
}
