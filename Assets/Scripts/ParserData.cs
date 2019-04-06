using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Objects.Data
{
    public class ParserData : MonoBehaviour
    {
        public static ParserData Instance;

        public float base_mass;
        public Vector3 k_speed;

        public void Awake()
        {
            Instance = this;
        }
    }
}

