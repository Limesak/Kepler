using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Shop Item/Dash")]
public class BuyDash : ShopObject
{
    public override void BuyThisItem()
    {
        OnBuyingDash?.Invoke();
    }

    public static event Action OnBuyingDash;
}