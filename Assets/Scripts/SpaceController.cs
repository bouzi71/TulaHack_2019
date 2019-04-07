using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.Control
{
    public class SpaceController : MonoBehaviour
    {
        public static SpaceController Instance;

        public const float step_time = 0.02f;

        public List<Data.SpaceObject> spaceObjects;


        private void Awake()
        {
            Time.timeScale = 10;
            Instance = this;
        }

        void Start()
        {
            foreach (var obj in spaceObjects) obj.Create();

            InvokeRepeating("Step", step_time, step_time);
        }

        private void Step()
        {
            foreach (var objectA in spaceObjects)
            {
                foreach (var objectB in spaceObjects)
                {
                    if (objectB == objectA) continue;

                    Vector3 d_rad_A = objectB.transform.position - objectA.transform.position;//Радиус вектор для объект_А в точку объект_B
                    float pow_2_rad = Mathf.Pow(d_rad_A.magnitude, 2);//квадрат расстояния между точками

                    if (pow_2_rad <= Data.SpaceObject.min_rad) pow_2_rad = Data.SpaceObject.min_rad;

                    objectA.Acceleration = (objectB.Gm / pow_2_rad) * d_rad_A.normalized;//Вектор ускорения [a] = (G*mass другого объекта / радиус квадрат)* единичное направление вектора
                }
            }

            foreach (var obj in spaceObjects)
                obj.Positionize();
        }

        public void Restart()
        {
            CancelInvoke("Step");

            foreach (var obj in spaceObjects)
                obj.Load();
            ClearAllTrails();

            InvokeRepeating("Step", step_time, step_time);
        }




        //
        // Очистка всех трейлов.
        //
        public void ClearAllTrails()
        {
            foreach (var obj in spaceObjects)
            {
                obj.GetComponentInChildren<TrailRenderer>().Clear();
            }
        }
    }
}

