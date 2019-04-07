using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.Visual
{
    public class CanvasCatcher : MonoBehaviour
    {
        public static CanvasCatcher Instance;

        public bool detail = false;

        [System.Serializable]
        public class CameraData
        {
            public Transform parent;

            public Vector3 position;
            public Quaternion quater;

            public float far;
            public Animator anim;
        }

        [SerializeField]
        private Camera main_camera;


        [SerializeField]
        private CameraData startData;
        [SerializeField]
        private List<CameraData> datas;


        private void Awake()
        {
            Instance = this;
        }

        public void BaseCamera()
        {
            main_camera.transform.SetParent(startData.parent);
            main_camera.transform.localPosition = startData.position;
            main_camera.transform.localRotation = startData.quater;
            main_camera.GetComponent<Camera>().farClipPlane = startData.far;

            DropAnimtions();

            detail = false;
        }

        public void SetCamera(Transform _parent)
        {
            var data = datas.Find(el => el.parent == _parent);

            main_camera.transform.SetParent(data.parent);
            main_camera.transform.localPosition = data.position;
            main_camera.transform.localRotation = data.quater;
            main_camera.GetComponent<Camera>().farClipPlane = data.far;

            //DropAnimtions();

            detail = data.anim != null;
            if (data.anim == null) DropAnimtions();
            else
            {
                data.anim.SetTrigger("Start");
            }
        }

        private void DropAnimtions()
        {
            foreach (var data in datas) data.anim.SetTrigger("Out");
        }

               
    }
}

