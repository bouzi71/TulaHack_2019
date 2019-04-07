using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.Control.Create
{
    public class ObjectCreator : MonoBehaviour
    {
        // Consts
        public static string cMeteoritObjectName = "MeteoritObject";


        public static ObjectCreator Instance;

        [Header("Object data")]
        [SerializeField]
        public float mass = 100000.0f;

        [SerializeField]
        public float speed = 0.2f;


        [Header("Scene data")]
        [SerializeField]
        private Transform space;
        [SerializeField]
        private Transform point0;
        [SerializeField]
        private Transform point1;

        private void Awake()
        {
            Instance = this;
        }

        public void Create()
        {
            GameObject new_obj = Instantiate(Resources.Load("SpaceObjects/" + cMeteoritObjectName, typeof(GameObject)), space) as GameObject;
            new_obj.transform.position = point0.position;

            var sp_obj = new_obj.transform.GetComponent<Data.SpaceObject>();
            sp_obj.v = (point1.position - point0.position).normalized * speed;
            sp_obj.k_mass = mass;
            sp_obj.Create();
            SpaceController.Instance.spaceObjects.Add(sp_obj);
        }

        public void Create(float v, float m)
        {
            GameObject new_obj = Instantiate(Resources.Load("SpaceObjects/" + cMeteoritObjectName, typeof(GameObject)), space) as GameObject;
            new_obj.transform.position = point0.position;

            var sp_obj = new_obj.transform.GetComponent<Data.SpaceObject>();
            sp_obj.v = (point1.position - point0.position).normalized * v;
            sp_obj.k_mass = m;
            sp_obj.Create();
            SpaceController.Instance.spaceObjects.Add(sp_obj);
        }

    }
}

