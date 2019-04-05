using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //[SerializeField]
    //private Transform space;
    ////var object1 = Instantiate(Resources.Load("SpaceObjects/Object1", typeof(GameObject)), space) as GameObject;


    public const float step_time = 0.02f;

    [SerializeField]
    public List<SpaceObject> spaceObjects;


    // Start is called before the first frame update
    
    void Start()
    {
        InvokeRepeating("Step", 0, step_time);   
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

                if (pow_2_rad <= 0.00001) pow_2_rad = 0.00001f;
                //Упростить расчетом Vector3 =  (G*d_rad_A) / pow_2_rad;

                objectA.Acceleration = (objectB.Gm / pow_2_rad) * d_rad_A.normalized;//Вектор ускорения [a] = (G*mass другого объекта / радиус квадрат)* единичное направление вектора
                //objectB.Acceleration = -1 * (G*objectA.mass / pow_2_rad+0.01f) * d_rad_A.normalized;//Вектор ускорение [a] другого объекта (-1 определяет противоположное направление радиус вектора
            }
        }

        foreach (var obj in spaceObjects)
            obj.Positionize();
    }
}
