using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

namespace CartLogic
{
    public class ShoppingCart : MonoBehaviour, IShoppingCart
    {
        public static ShoppingCart Instance { get; private set; }

        private HashSet<ItemInfo> cartItems = new HashSet<ItemInfo>();
        private HashSet<ItemInfo> removedItems = new HashSet<ItemInfo>();
        public event Action<ISet<ItemInfo>> OnContentChanged = set => { };


        // sa am o fucntie in care sa am un array cu 2 elemente. daca pot lua alea doa obiecte sa le pun pe raft, continuam
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

        private void OnTriggerEnter(Collider other)
        {
            var item = other.GetComponent<ItemInfo>();
            if (item != null)
            {
                item.ItemInCart = true;
                AddItem(item);
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            ItemInfo item = other.GetComponent<ItemInfo>();

            if (item != null)
            {
                item.ItemInCart = false;
                RemoveItem(item);
            }
        }

        /**
        * @brief Adauga un obiect in cos
        */
        public void AddItem(ItemInfo item)
        {
            cartItems.Add(item);
            OnContentChanged(cartItems);
            Debug.Log("Successfully added " + item.ItemName);
        }

        /**
        * @brief Scoate un obiect din cos
        */
        public void RemoveItem(ItemInfo item)
        {
            cartItems.Remove(item);
            removedItems.Add(item);
            OnContentChanged(cartItems);
            //Debug.Log("Eliminat cu succes!");
            // Debug.Log();
        }

        /**
        * @brief Returneaza o lista cu obiectele curente aflate in cos
        */
        public HashSet<ItemInfo> GetCartItems()
        {
            return cartItems;
        }

        /**
        * @brief Returneaza o lista cu obiectele scoase
        */
        public HashSet<ItemInfo> GetRemovedItems()
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
}