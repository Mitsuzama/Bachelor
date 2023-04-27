using Item;
using System.Collections.Generic;

namespace CartLogic
{
    public interface IShoppingCart
    {
        void AddItem(ItemInfo item);
        void RemoveItem(ItemInfo item);
        HashSet<ItemInfo> GetCartItems();
        void ClearCart();
    }
}