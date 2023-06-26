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
        Rigidbody m_Rigidbody;
        public float m_Thrust = 20f;

        public static ShoppingCart Instance { get; private set; }

        private HashSet<ItemInfo> cartItems = new HashSet<ItemInfo>();
        private HashSet<ItemInfo> removedItems = new HashSet<ItemInfo>();
        public event Action<ISet<ItemInfo>> OnContentChanged = set => { };

        public Rigidbody child;
        public GameObject parent;

        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }


        // sa am o functie in care sa am un array cu 2 elemente. daca pot lua alea doa obiecte sa le pun pe raft, continuam
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
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }

        /// ////////////////////////////////////////////////////////////////////////////////////
        //[SerializeField] private CartItemTracker cartItemTracker; 


        /*private void MakeItemsIndependent()
        {
            List<Item> itemList = cartItemTracker.ItemsList;
            foreach (Item item in itemList)
            {
                item.transform.parent = null;
                item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }*/

        /*private void MakeItemsChildren()
        {
            List<ItemInfo> itemList = cartItemTracker.ItemList;
            foreach (temInfo item in itemList)
            {
                item.transform.parent = transform;
                Rigidbody childRigidbody = childObject.GetComponent<Rigidbody>();
                childRigidbody.isKinematic = true;
                childRigidbody.velocity = Vector3.zero;
                childRigidbody.angularVelocity = Vector3.zero;
            }
        }*/

        /*private float GetObjectHeightBelow(GameObject obj)
        {
            RaycastHit hit;
            if (Physics.Raycast(obj.transform.position, Vector3.down, out hit))
            {
                return hit.collider.bounds.size.y;
            }
            return 0f;
        }*/
        /*private float currentHeight = 0f;
        public void PlaceObject(GameObject obj)
        {
            float objectWidth = obj.GetComponent<Renderer>().bounds.size.x;
            float objectDepth = obj.GetComponent<Renderer>().bounds.size.z;
            float objectHeight = obj.GetComponent<Renderer>().bounds.size.y;

            float rectangleWidth = transform.localScale.x;
            float rectangleDepth = transform.localScale.z;

            if (rectangleWidth >= objectWidth && rectangleDepth >= objectDepth)
            {
                // Position the object on top of the rectangle at the current height
                Vector3 newPosition = transform.position;
                newPosition.y += currentHeight + (objectHeight / 2);
                obj.transform.position = newPosition;

                // Update the current height
                currentHeight += objectHeight;

                // Reduce the available space on the rectangle
                rectangleWidth -= objectWidth;
                rectangleDepth -= objectDepth;
            }
            else
            {
                // Move the object upwards by the height of the object below it
                float objectHeightBelow = GetObjectHeightBelow(obj);

                Vector3 newPosition = obj.transform.position;
                newPosition.y += objectHeightBelow;
                obj.transform.position = newPosition;

                // Update the current height
                currentHeight = obj.transform.position.y + objectHeight;
            }
        }*/

        public void SetParent(Collider childObject, GameObject parentObject)
        {
            var childRigidbody = childObject.GetComponent<Rigidbody>();
            if (childRigidbody)
            {
                if (childRigidbody.isKinematic == false)
                {
                    childObject.transform.parent = parentObject.transform;
                    //childObject.transform.position = parentObject.transform.position;
                    //childRigidbody.isKinematic = true;
                    childRigidbody.velocity = Vector3.zero;
                    childRigidbody.angularVelocity = Vector3.zero;
                    PlaceObject(childObject.gameObject);
                }
            }   
        }
        
        /*void Update()
        {
            var vect = new Vector3(0.0f, 1.0f, 0.0f);
            child.transform.position = parent.transform.position + vect;
        }*/

        private void OnTriggerStay(Collider other)
        {
            var item = other.GetComponent<ItemInfo>();
            if (item != null && item.ItemInCart == false)
            {
                item.ItemInCart = true;
                AddItem(item);
                child = other.GetComponent<Rigidbody>();
                parent = gameObject;
                SetParent(other, gameObject);
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
            //adaug ca si copil
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