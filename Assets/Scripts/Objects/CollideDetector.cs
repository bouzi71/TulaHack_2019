using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Objects.Data
{
    public class CollideDetector : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(string.Format("[{0}] [{1}] collided at '{2}'",
                gameObject.name,
                other.name,
                Time.time));
        }
    }
}