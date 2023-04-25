using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartItemCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AddItem(other);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveItem(other);
    }
}
