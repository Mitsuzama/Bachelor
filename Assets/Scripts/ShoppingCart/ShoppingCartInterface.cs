using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CartLogic
{
    /*
    public static class EnumerableExtensions
    {
        public static string JoinString<T>(this IEnumerable<T> sequence, string seperator, Func<T, string> selector)
        {
            return string.Join(seperator, sequence.Select(selector).ToArray());
        }
    }
    */

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
                prices.text = $"${items.Sum(component => component.ItemPrice):F2}";
            };
        }
    }
}
    /*
    public class ShoppingCartInterface : MonoBehaviour 
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
                tmpList += $"{pair.Value} x {pair.Key}\n";
            }
            addedItems.text = tmpList;

            float totalPrice = 0;
            foreach (var pair in itemCounts)
            {
                float itemPrice = 0;
                foreach (var item in itemsContained)
                {
                    if (item.itemName == pair.Key)
                    {
                        itemPrice = item.itemPrice;
                        break;
                    }
                }
                totalPrice += pair.Value * itemPrice;
            }
            var tmpPrices = $"{totalPrice} Lei";
            prices.text = tmpPrices;

        }
    }
    */
