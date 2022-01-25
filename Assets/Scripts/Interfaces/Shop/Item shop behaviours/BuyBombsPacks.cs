using UnityEngine;
using System;

[CreateAssetMenu(menuName = ("Shop Item/Bombs"))]
public class BuyBombsPacks : ShopObject
{
    public int thisBombsPack { get; protected set; }

    public override void BuyThisItem()
    {
        OnBuyingBombsPack(thisBombsPack);
    }

    public static event Action<int> OnBuyingBombsPack;
}
