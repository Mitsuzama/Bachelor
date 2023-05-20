using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Item
{
    /**
    * @brief Script atasat la fiecare element din magazin, folosit pentru a seta pretul si alte informatii despre element
    */
    public class ItemInfo : MonoBehaviour
    {
        [Tooltip("Varabila folosita in logarea datelor ce spune daca oiectul este sau nu in cos")]
        private bool itemInCart = false;

        [Tooltip("Numele alimentului din magazin")]
        [SerializeField] private string itemName;

        [Tooltip("Descrierea alimentului")]
        [SerializeField] private string itemDescription;

        [Tooltip("Informatii nutritionale")]
        [SerializeField] private NutritionalInfo itemNutritionalInfo;

        [Tooltip("Pretul alimentului")]
        [SerializeField] private float itemPrice;

        [Tooltip("Reducere la aliment")]
        [SerializeField] private bool isOnSale;

        /*Getters and Setters*/
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public string ItemDescription
        {
            get { return itemDescription; }
            set { itemDescription = value; }
        }

        public NutritionalInfo ItemNutritionalInfo
        {
            get { return itemNutritionalInfo; }
            set { itemNutritionalInfo = value; }
        }

        public float ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }

        public bool IsOnSale
        {
            get { return isOnSale; }
            set { isOnSale = value; }
        }

        /**
        * @brief Metoda folosita pentru setarea proprietatilor alimentului
        */
        public void SetItemProperties(string name, string description, float price, NutritionalInfo nutritionalInfo)
        {
            itemName = name;
            itemDescription = description;
            itemPrice = price;
            itemNutritionalInfo = nutritionalInfo;
        }

        public bool ItemInCart
        {
            get { return itemInCart; }
            set { itemInCart = value; }
        }
    }
}
