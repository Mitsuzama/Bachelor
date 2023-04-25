using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingCartUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text addedItems;
    [SerializeField]
    private TMP_Text prices;
    [SerializeField]
    private CartItemCollider cartContent;

    private void Awake()
    {
        items += OnContentChanged;
    }

    private void OnContentChanged(ISet<ItemInfo> itemsContained)
    {
        Dictionary<string, int> itemCounts = new Dictionary<string, int>();
        foreach (var item in itemsContained)
        {
            if (itemCounts.ContainsKey(item.Name))
            {
                itemCounts[item.Name]++;
            }
            else
            {
                itemCounts[item.Name] = 1;
            }
        }
        var tmpList = string.Join(itemCounts.Select(pair => $"{pair.Value} {pair.Key}", "\n");
        addedItems.text = tmpList;

        var tmpPrices = string.Join($"{items.Sum(item => item.Price)}", " Lei");
        prices.text = tmpPrices;
    }
}
