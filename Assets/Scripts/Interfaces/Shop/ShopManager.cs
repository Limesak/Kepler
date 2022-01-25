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
}
