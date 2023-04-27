using System;
using System.Globalization;
using UnityEngine;
using TMPro;
using Item;

namespace InfoShow
{
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

        /* preluat si compus de aici: https://answers.unity.com/questions/17968/finding-the-bounds-of-a-grouped-model.html */
        /**
         * @rief: calculeaza marginile unui model grupat. Utilizat pentru un model realizat din mai multe sub-modele
         * 
         * @param: gameObject: obiectul pentru care sunt calculate mariginile (bounds)
        */
        [Obsolete]
        public static Bounds CalculateLocalBounds(GameObject gameObject)
        {
            var bounds = gameObject.GetComponentsInChildren<MeshFilter>(true)
                .Where(mf => filter == null || filter(mf))
                .Select(mf =>
                {
                    var mesh = isSharedMesh ? mf.sharedMesh : mf.mesh;
                    var localBound = mf.transform.TransformBounds(gameObject.transform, mesh.bounds);
                    return localBound;
                })
                .Where(b => b.size != Vector3.zero)
                .ToArray();

            if (bounds.Length == 0)
                return new Bounds();

            if (bounds.Length == 1)
                return bounds[0];

            var compositeBounds = bounds[0];

            for (var i = 1; i < bounds.Length; i++)
            {
                compositeBounds.Encapsulate(bounds[i]);
            }

            return compositeBounds;
        }

        private void LateUpdate()
        {
            if (item)
            {
                var itemCenter = item.transform.TransformPoint(focusedItemBounds.Center);
                transform.position = itemCenter - camera.transform.right * (focusedItemBounds.size.magnitude/2 + offset);
                transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
            }
        }
    }
}