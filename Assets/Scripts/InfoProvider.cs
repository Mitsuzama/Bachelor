using System;
using System.Globalization;
using UnityEngine;
using System.Linq;
using TMPro;
using Item;

namespace InfoShow
{
    public static class BoundsExtensions
    {
        /* preluat si compus de aici: https://answers.unity.com/questions/17968/finding-the-bounds-of-a-grouped-model.html */
        /**
         * @rief: calculeaza marginile unui model grupat. Utilizat pentru un model realizat din mai multe sub-modele
         * 
         * @param: gameObject: obiectul pentru care sunt calculate mariginile (bounds)
        */
        public static Bounds CalculateLocalBounds(this GameObject gameObject)
        {
            Quaternion currentRotation = gameObject.transform.rotation; // rotatia curenta
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            Bounds bounds = new Bounds(gameObject.transform.position, Vector3.zero);
            foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>())
            {
                bounds.Encapsulate(renderer.bounds);
            }

            Vector3 localCenter = bounds.center - gameObject.transform.position;
            bounds.center = localCenter;

            gameObject.transform.rotation = currentRotation;

            return bounds;
        }
    }

    public class InfoProvider : MonoBehaviour, IInfoProvider
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TMP_Text infoText;
        [SerializeField] private Camera camera;
        [SerializeField] private float offset;

        private ItemInfo item;
        private Bounds focusedItemBounds;

        public ItemInfo Item
        {
            get => item;
            set
            {
                if (item != value)
                {
                    item = value;
                    UpdateInfoText();
                }
            }
        }

        private void UpdateInfoText()
        {
            if (item)
            {
                focusedItemBounds = item.gameObject.CalculateLocalBounds();
                infoText.text = $"{item.ItemName}\n{item.ItemPrice.ToString("C", new CultureInfo("ro-RO"))}";
                canvas.enabled = true;
            }
            else
            {
                canvas.enabled = false;
            }
        }

        private void LateUpdate()
        {
            if (item)
            {
                var itemCenter = item.transform.TransformPoint(focusedItemBounds.center);
                transform.position = itemCenter - camera.transform.right * (focusedItemBounds.size.magnitude/2 + offset);
                transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
            }
        }
    }
}