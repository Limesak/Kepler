using System.Collections.Generic;
using AsteroidBelt.Interfaces.Shop.Item_shop_behaviours;
using AsteroidBelt.Player_Scripts;
using UnityEngine;

namespace AsteroidBelt.Interfaces.Shop
{
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
            List<ShopObject> objectsToBuy = new List<ShopObject>();
            foreach(ShopObject obj in itemsToBuy){
                switch (main.playerHasBombs){
                    case true:
                        objectsToBuy.Add(obj);
                        break;
                    case false:
                        if(!obj.needsBombLauncher){
                            objectsToBuy.Add(obj);
                        }
                        break;
                }
            }

            for(int i = 0; i < itemSlots.Length; i++){
                ShopTemplate selectedTemplate = itemSlots[i].GetComponent<ShopTemplate>();
                selectedTemplate.bought = false;

                int randomNum = Random.Range(0, objectsToBuy.Count);
                var newItem = objectsToBuy[randomNum];

                if(newItem.buyOneAtATime){
                    objectsToBuy.Remove(newItem);
                }

                selectedTemplate.titleTxt.text = newItem.objectName;
                selectedTemplate.cost = newItem.costInGold;
                selectedTemplate.costTxt.text = selectedTemplate.cost.ToString();
                selectedTemplate.shopObject = newItem;
            }
        }

        [ContextMenu("buy bombs")]
        private void HandleBombauncherSell(){
            main.player.GetComponent<Player>().hasBombs = true;
            main.playerHasBombs = true;
            GainBombs(3);
        }

        private void GainBombs(int bombsRegained){
            if(main.player.GetComponent<Player>().bombsAmmo < main.player.GetComponent<Player>().maxBombAmmo){
                main.player.GetComponent<Player>().bombsAmmo += bombsRegained;
            }
            if(main.player.GetComponent<Player>().bombsAmmo > main.player.GetComponent<Player>().maxBombAmmo){
                main.player.GetComponent<Player>().bombsAmmo = main.player.GetComponent<Player>().maxBombAmmo;
            }
        }

        [ContextMenu("buy dash")]
        private void HandleDashSell(){
            main.player.GetComponent<Player>().hasDash = true;
        }

        [ContextMenu("buy double shot")]
        private void HandleDoubleShotSell(){
            main.player.GetComponent<Player>().hasDoubleShot = true;
        }
    }
}
