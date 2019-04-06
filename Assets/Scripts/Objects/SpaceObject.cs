using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.Data
{
    public class SpaceObject : MonoBehaviour
    {
        //public static float G = 6.67f * Mathf.Pow(10, -11);//[(Н * м^2) / (кг^2)]
        public static float G = 6.67e-11f;//[(Н * м^2) / (кг^2)]
        public static float G_km = 6.66e-5f;//[(Н * км^2) / (кг^2)]
        public static float G_km_scale_e6 = 6.66e-23f;//[(Н * км^2) / (кг^2)]


        public const float min_rad = 0.01f;

        [SerializeField]
        public float k_mass; //[кг]

        [Header("Physics components")]
        public Vector3 v;//[м/с
        private Vector3 a;//[м^2/c]


        [HideInInspector]
        public float Gm;

        private void Start()
        {
            G_mass_override(G_km * k_mass * ParserData.Instance.base_mass);
            v = new Vector3(v.x * ParserData.Instance.k_speed.x, v.y * ParserData.Instance.k_speed.y, v.z * ParserData.Instance.k_speed.z);
            Save();
        }




        #region Position events
        /// <summary>
        /// при установке сразу изменяет вектор скорости (предопеределен SpaceController.step_time)
        /// </summary>
        public Vector3 Acceleration
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
                v += a * Control.SpaceController.step_time;
            }
        }
        /// <summary>
        /// Устанавливает координату (предопеределен SpaceController.step_time)
        /// </summary>
        public void Positionize()
        {
            transform.position += v * Control.SpaceController.step_time;
        }
        #endregion

        #region Mass
        private void G_mass_override(float G_mass)
        {
            Gm += G_mass;
        }
        #endregion


        private struct Settings
        {
            public float Gm;
            public Vector3 pos;
            public Vector3 v;
        }
        private Settings settings;

        public void Save()
        {
            settings = new Settings { Gm = Gm, pos = transform.position, v = v };
        }

        public void Load()
        {
            Gm = settings.Gm;
            transform.position = settings.pos;
            v = settings.v;
            a = Vector3.zero;
        }

    }
}

