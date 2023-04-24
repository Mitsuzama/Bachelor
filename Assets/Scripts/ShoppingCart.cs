using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingCart : MonoBehaviour
{
    public static ShoppingCart Instance { get; private set; }

    private List<ItemInfo> cartItems = new List<ItemInfo>();
    private List<ItemInfo> removedItems = new List<ItemInfo>();

    /**
     * @brief Verific daca am deja o instanta a caruciorului
     */
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /**
     * @brief Adauga un obiect in cos
     */
    public void AddItem(GameObject itemObject)
    {
        ItemInfo item = itemObject.GetComponent<ItemInfo>();
        cartItems.Add(item);
        Debug.Log("Adaugat cu succes!");
    }

    /**
     * @brief Scoate un obiect din cos
     */
    public void RemoveItem(GameObject itemObject)
    {
        ItemInfo item = itemObject.GetComponent<ItemInfo>();
        if (cartItems.Contains(item))
        {
            cartItems.Remove(item);
            removedItems.Add(item);
        }
        else if (removedItems.Contains(item))
        {
            removedItems.Remove(item);
        }
        Debug.Log("Eliminat cu succes!");
        // Debug.Log();
    }

    /**
     * @brief Returneaza o lista cu obiectele curente aflate in cos
     */
    public List<ItemInfo> GetCartItems()
    {
        return cartItems;
    }

    /**
     * @brief Returneaza o lista cu obiectele scoase
     */
    public List<ItemInfo> GetRemovedItems()
    {
        return removedItems;
    }

    /**
     * @brief Golesc cosul
     */
    public void ClearCart()
    {
        cartItems.Clear();
        removedItems.Clear();
    }
}
