using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Shop Item/Bomb launcher")]
public class BuyBombLauncher : ShopObject
{
    public override void BuyThisItem(){
        UnlockBombs();
    }

    private void UnlockBombs(){
        OnUnlockBombs?.Invoke();
    }

    public static event Action OnUnlockBombs;
}