﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects.Control
{
    public class Controller : MonoBehaviour
    {
        public static Controller Instance;

        public const float step_time = 0.02f;

        public List<Data.SpaceObject> spaceObjects;


        private void Awake()
        {
            Time.timeScale = 20;
            Instance = this;
        }

        void Start()
        {
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
    }
}

