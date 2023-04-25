using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ShoppingCart
{

    public class ShoppingCartUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text addedItems;
        [SerializeField]
        private TMP_Text prices;
        [SerializeField]
        private ShoppingCart cartContent;

        private void Awake()
        {
            cartContent.OnContentChanged += OnContentChanged;
        }

        private void OnContentChanged(HashSet<ItemInfo> itemsContained)
        {
            Dictionary<string, int> itemCounts = new Dictionary<string, int>();
            foreach (var item in itemsContained)
            {
                if (itemCounts.ContainsKey(item.itemName))
                {
                    itemCounts[item.itemName]++;
                }
                else
                {
                    itemCounts[item.itemName] = 1;
                }
            }

            if (itemCounts == null)
            {
                throw new ArgumentNullException(nameof(itemCounts));
            }

            var tmpList = "";
            foreach (var pair in itemCounts)
            {
                tmpList += $"{pair.Value} {pair.Key}\n";
            }
            addedItems.text = tmpList;

            var totalPrice = 0;
            foreach (var pair in itemCounts)
            {
                var itemPrice = itemsContained.FirstOrDefault(item => item.itemName == pair.Key).itemPrice;
                totalPrice += pair.Value * itemPrice;
            }
            var tmpPrices = $"{totalPrice} Lei";
            prices.text = tmpPrices;

        }
    }
}
