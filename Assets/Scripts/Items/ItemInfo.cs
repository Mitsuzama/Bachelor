using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

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

        private void FetchObjectName()
        {
            string gameObjectName = gameObject.name;
            gameObjectName = Regex.Replace(gameObjectName, @"[\(\)\d_]", "");

            if (string.IsNullOrEmpty(gameObjectName))
                gameObjectName = " ";

            char[] chars = gameObjectName.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            itemName = new string(chars);

            itemPrice = Random.Range(0f, 15f);
        }

        private void FetchObjectData()
        {
            itemNutritionalInfo.grasimi = Random.Range(0f, 10f);
            itemNutritionalInfo.valoareEnergetica = Random.Range(100f, 500f);
            itemNutritionalInfo.glucide = Random.Range(20f, 100f);
            itemNutritionalInfo.fibre = Random.Range(1f, 3f);
            itemNutritionalInfo.sodiu = Random.Range(0f, 100f);
            itemNutritionalInfo.proteine = Random.Range(5f, 30f);
            itemNutritionalInfo.grasimiSaturate = Random.Range(0f, 5f);
            
            if(itemDescription.Contains("Apa"))
            {
                itemNutritionalInfo.grasimi = 0;
                itemNutritionalInfo.valoareEnergetica = 0;
                itemNutritionalInfo.glucide = 0;
                itemNutritionalInfo.fibre = 0;
                itemNutritionalInfo.sodiu = 0;
                itemNutritionalInfo.proteine = 0;
                itemNutritionalInfo.grasimiSaturate = 0;
            }
        }

        ///setare automata a numelui
        private void Awake()
        {
            FetchObjectName();
            FetchObjectData();
        }
    }
}
