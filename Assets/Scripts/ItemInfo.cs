using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct NutritionalInfo
{
    public float grasimi;
    public float valoareEnergetica;
    public float glucide;
    public float fibre;
    public float sodiu;
    public float proteine;
    public float grasimiSaturate;
}

/**
* @brief Script atasat la fiecare element din magazin, folosit pentru a seta pretul si alte informatii despre element
*/
public class ItemInfo : MonoBehaviour
{
    [Tooltip("Numele alimentului din magazin")]
    public string itemName { get; set; }

    [Tooltip("Descrierea alimentului")]
    public string itemDescription { get; set; }

    [Tooltip("Informatii nutritionale")]
    public NutritionalInfo itemNutritionalInfo { get; set; }

    [Tooltip("Pretul alimentului")]
    public float itemPrice { get; set; }

    [Tooltip("Reducere la aliment")]
    public bool isOnSale { get; set; }

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

    /**
     * @brief Sets the text of a TextMeshProUGUI component to the item price.
     */
    private void Start()
    {
        // componenta TextMeshProUGUI
        /*TextMeshPro textObject = GetComponentInChildren<TextMeshPro>();
        textObject.SetText(" ");
        
        string toShow = itemName + "\nPret: " + itemPrice.ToString("C") + " Lei";
        if(isOnSale)
        {
            toShow += "\nOFERTA";
        }

        textObject.SetText(toShow);*/
    }

}
