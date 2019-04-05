using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    public static float G = 6.67f * Mathf.Pow(10, -11);//[(Н * м^2) / (кг^2)]
    public const float min_rad = 0.01f;

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
        G_mass_override(G * mass);
    }


    #region Position events
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
    #endregion

    #region Mass
    private void G_mass_override(float G_mass)
    {
        Gm += G_mass;
    }
    #endregion

    //private void OnTriggerEnter(Collider other)
    //{
        
    //}

    private void OnTriggerStay(Collider other)
    {
        //id triger?
        if ((other.transform.position - transform.position).magnitude > min_rad) return;

        var other_SpObj = other.GetComponent<SpaceObject>();
        if (other_SpObj.Gm > Gm)
        {
            Destroy(gameObject, Controller.step_time);
            Controller.Instance.spaceObjects.Remove(this);
            return;
        }
        else
        {
            G_mass_override(other_SpObj.Gm);
        }

        Debug.Log(string.Format("Trigger [{0}]", gameObject.name));
    }

    //private void OnTriggerExit(Collider other)
    //{
        
    //}
}
