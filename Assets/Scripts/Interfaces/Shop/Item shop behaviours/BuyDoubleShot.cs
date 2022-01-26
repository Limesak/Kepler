using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Shop Item/DoubleShot")]
public class BuyDoubleShot : ShopObject
{
    public override void BuyThisItem(){
        OnUnlockDoubleShot?.Invoke();
    }

    public static event Action OnUnlockDoubleShot;
}
