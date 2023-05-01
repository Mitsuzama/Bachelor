using UnityEngine;
using UnityEngine.XR;

namespace InfoShow
{
    public class GrabObject : MonoBehaviour
    {
        private Rigidbody grabbed;

        public Rigidbody Grabbed
        {
            get { return grabbed; }
            set { grabbed = value; }
        }
    }
}