using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopTemplate : MonoBehaviour
{
    public TMP_Text titleTxt;
    public TMP_Text costTxt;
    public int cost;
    public Image spriteOfItem;

    Main main;
    ShopManager shopManager;
    [HideInInspector] public ShopObject shopObject;

    void Start(){
        main = Main.Instance;
        shopManager = ShopManager.Instance;
    }

    public void PurchaseItem(){
        if(main.currentScore > cost){
            main.currentScore -= cost;
            shopObject.BuyThisItem();
            shopManager.CloseShop();
        }
    }
}
