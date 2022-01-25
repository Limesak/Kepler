using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Shop Item/Bomb launcher")]
public class BuyBombLauncher : ShopObject
{
    public override void BuyThisItem()
    {
        OnBuyingBombLauncher?.Invoke();
    }

    public static event Action OnBuyingBombLauncher;
}
