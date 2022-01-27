using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidBelt.Interfaces.Shop
{
    public class ShopTemplate : MonoBehaviour
    {
        public TMP_Text titleTxt;
        public TMP_Text costTxt;
        public int cost;
        public Image spriteOfItem;
        [HideInInspector] public bool bought;

        Main main;
        ShopManager shopManager;
        [HideInInspector] public ShopObject shopObject;

        void Start(){
            main = Main.Instance;
            shopManager = ShopManager.Instance;
        }

        public void PurchaseItem(){
            if(main.currentScore >= cost && !bought){
                main.currentScore -= cost;
                shopObject.BuyThisItem();
                bought = true;
            }
        }
    }
}
