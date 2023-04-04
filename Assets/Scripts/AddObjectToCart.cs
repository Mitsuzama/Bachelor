using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using static UnityEditor.Progress;

public class AddObjectToCart : MonoBehaviour
{
    public AudioClip enterShopSound;
    public AudioClip exitShopSound;

    private AudioSource enterAudioSource;
    private AudioSource exitAudioSource;

    public GameObject shoppingCart;

    void Start()
    {
        enterAudioSource = gameObject.AddComponent<AudioSource>();
        exitAudioSource = gameObject.AddComponent<AudioSource>();

        enterAudioSource.clip = enterShopSound;
        exitAudioSource.clip = exitShopSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object collided with is an item that can be picked up
        if (other.CompareTag("Carucior"))
        {
            GameObject gameObject = other.gameObject;

            // Add the item to the shopping cart
            ShoppingCart.Instance.AddItem(gameObject);

            enterAudioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Carucior"))
        {
            GameObject gameObject = other.gameObject;

            // Add the item to the shopping cart
            ShoppingCart.Instance.RemoveItem(gameObject);

            exitAudioSource.Play();
        }
    }

}
