using System;
using UnityEngine;
using System.Linq;

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
}