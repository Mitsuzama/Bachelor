using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Item;

namespace InfoShow
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] private GrabObject handGrabber;

        [SerializeField] private InfoProvider infoProvider;

        private void Update()
        {
            var grabbedObject = handGrabber.Grabbed;
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