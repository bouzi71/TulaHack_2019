using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.Control.Create
{
    public class ObjectCreator : MonoBehaviour
    {
        public static ObjectCreator Instance;

        [Header("Object data")]
        public float mass;
        public float speed;


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
            GameObject new_obj = Instantiate(Resources.Load("SpaceObjects/NewObject", typeof(GameObject)), space) as GameObject;
            new_obj.transform.position = point0.position;

            var sp_obj = new_obj.transform.GetComponent<Data.SpaceObject>();
            sp_obj.v = (point1.position - point0.position).normalized * speed;
            sp_obj.k_mass = mass;
            sp_obj.Create();
            SpaceController.Instance.spaceObjects.Add(sp_obj);
        }
    }
}

