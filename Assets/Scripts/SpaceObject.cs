using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    public const float G = 1; // 6.67 * 10^(-11) [(Н * м^2) / (кг^2)]

    [SerializeField]
    public float mass; //[кг]

    [Header("Physics components")]
    public Vector3 v;//[м/с]
    [HideInInspector]
    public Vector3 a;//[м^2/c]


    [HideInInspector]
    public float Gm;


    private void Awake()
    {
        Gm = G * mass;
    }

    /// <summary>
    /// при установке сразу изменяет вектор скорости (предопеределен Controller.step_time)
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
            v += a * Controller.step_time;
        }
    }

    /// <summary>
    /// Устанавливает координату (предопеределен Controller.step_time)
    /// </summary>
    public void Positionize()
    {
        transform.position += v * Controller.step_time;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(string.Format("Collision [{0}]", gameObject.name));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("Trigger [{0}]", gameObject.name));
    }
}
