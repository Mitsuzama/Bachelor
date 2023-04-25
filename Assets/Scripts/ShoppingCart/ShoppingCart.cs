using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShoppingCart
{
    public class ShoppingCart : MonoBehaviour
    {
        public static ShoppingCart Instance { get; private set; }

        private HashSet<ItemInfo> cartItems = new HashSet<ItemInfo>();
        private HashSet<ItemInfo> removedItems = new HashSet<ItemInfo>();
        public event Action<HashSet<ItemInfo>> OnContentChanged = set => { };

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
            AddItem(other);
        }

        private void OnTriggerExit(Collider other)
        {
            RemoveItem(other);
        }

        /**
        * @brief Adauga un obiect in cos
        */
        public void AddItem(Collider itemObject)
        {
            ItemInfo item = itemObject.GetComponent<ItemInfo>();
            if(item != null)
            {
                cartItems.Add(item);
                OnContentChanged(cartItems);
            }
            Debug.Log("Adaugat cu succes!");
        }

        /**
        * @brief Scoate un obiect din cos
        */
        public void RemoveItem(Collider itemObject)
        {
            ItemInfo item = itemObject.GetComponent<ItemInfo>();
            if (cartItems.Contains(item))
            {
                cartItems.Remove(item);
                removedItems.Add(item);
                OnContentChanged(cartItems);
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