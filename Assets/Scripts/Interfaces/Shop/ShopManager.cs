using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : SingletonPersistent<ShopManager>
{
    public GameObject shopCanvas;
    public GameObject[] itemSlots;
    public ShopObject[] itemsToBuy;
    public bool shopIsOpen;

    Main main;

    public override void Awake(){
        base.Awake();
    }
    
    void Start(){
        main = Main.Instance;
    }

    void OnEnable(){
        BuyBombLauncher.OnUnlockBombs += HandleBombauncherSell;
        BuyBombsPacks.OnBombsBought += GainBombs;
        BuyDash.OnUnlockDash += HandleDashSell;
        BuyDoubleShot.OnUnlockDoubleShot += HandleDoubleShotSell;
    }

    void OnDisable(){
        BuyBombLauncher.OnUnlockBombs -= HandleBombauncherSell;
        BuyBombsPacks.OnBombsBought -= GainBombs;
        BuyDash.OnUnlockDash -= HandleDashSell;
        BuyDoubleShot.OnUnlockDoubleShot -= HandleDoubleShotSell;
    }

    public void OpenShop(){
        main.PauseGame();
        UpdateTheSlots();
        shopCanvas.SetActive(true);
        shopIsOpen = true;
    }

    public void CloseShop(){
        shopCanvas.SetActive(false);
        shopIsOpen = false;
        main.NextLevel();
        main.ResumeGame();
    }

    public void UpdateTheSlots(){
        for(int i = 0; i < itemSlots.Length; i++){
            int randomNum = Random.Range(0, itemsToBuy.Length);
            var newItem = itemsToBuy[randomNum];

            itemSlots[i].GetComponent<ShopTemplate>().titleTxt.text = newItem.objectName;
            itemSlots[i].GetComponent<ShopTemplate>().cost = newItem.costInGold;
            itemSlots[i].GetComponent<ShopTemplate>().costTxt.text = itemSlots[i].GetComponent<ShopTemplate>().cost.ToString();
            itemSlots[i].GetComponent<ShopTemplate>().shopObject = newItem;
        }
    }

    private void HandleBombauncherSell(){
        main.player.GetComponent<Player>().hasBombs = true;
    }

    private void GainBombs(int bombsRegained){
        if(main.player.GetComponent<Player>().bombsAmmo < main.player.GetComponent<Player>().maxBombAmmo){
            main.player.GetComponent<Player>().bombsAmmo += bombsRegained;
        }
        if(main.player.GetComponent<Player>().bombsAmmo > main.player.GetComponent<Player>().maxBombAmmo){
            main.player.GetComponent<Player>().bombsAmmo = main.player.GetComponent<Player>().maxBombAmmo;
        }
    }

    private void HandleDashSell(){
        main.player.GetComponent<Player>().hasDash = true;
    }

    private void HandleDoubleShotSell(){
        main.player.GetComponent<Player>().hasDoubleShot = true;
        main.playerHasBombs = true;
    }
}
