using UnityEngine;

[CreateAssetMenu(menuName = ("Shop Item/Bombs"))]
public class BuyBombsPacks : ShopObject
{
    public int bombsRegained;

    public override void BuyThisItem()
    {
        GainBombs();
    }

    private void GainBombs(){
        if(player.bombsAmmo < player.maxBombAmmo){
            player.bombsAmmo += bombsRegained;
        }
        if(player.bombsAmmo > player.maxBombAmmo){
            player.bombsAmmo = player.maxBombAmmo;
        }
    }
}
