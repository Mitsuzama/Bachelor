using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CartLogic
{
    public class ShoppingCartInterface : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text addedItems;
        [SerializeField]
        private TMP_Text prices;
        [SerializeField]
        private ShoppingCart cartContent;

        /**
         * @brief   Unity event function that is called when the script instance is being loaded.
                    Registers an event handler to update the shopping cart interface when the cart content changes.
                    
                    Called when the script instance is being loaded. Initializes the interface and subscribes to the OnContentChanged event of the shopping cart.
         */
        private void Awake()
        {
            cartContent.OnContentChanged += items =>
            {
                addedItems.text = string.Join("\n", items
                    .GroupBy(i => i.ItemName)
                    .Select(grouping => $"{grouping.Count()}x {grouping.Key}"));

                //addedItems.text = items.GroupBy(i => i.itemName).JoinString("\n", grouping => $"{grouping.Count()}x {grouping.Key}");
                prices.text = $"{items.Sum(component => component.ItemPrice):F2} Lei";
            };
        }
    }
}
