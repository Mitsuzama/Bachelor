using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Item;

namespace InfoShow
{
    public class Grabber : MonoBehaviour
    {
        [SerializeField] private XRController handGrabber;

        [SerializeField] private Component infoProvider;

        private void Update()
        {
            var grabbedObject = handGrabber.selectUsage;
            if (grabbedObject && grabbedObject.GetComponent<ItemInfo>())
            {
                ((InfoProvider)infoProvider).Item = grabbedObject.GetComponent<ItemInfo>();
            }
            else
            {
                ((InfoProvider)infoProvider).Item = null;
            }
        }
    }
}