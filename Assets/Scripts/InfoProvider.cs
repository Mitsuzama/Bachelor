using System;
using System.Globalization;
using UnityEngine;
using System.Linq;
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