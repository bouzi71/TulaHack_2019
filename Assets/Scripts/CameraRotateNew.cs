using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateNew : MonoBehaviour
{
    public Transform CamerA;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        CamerA.eulerAngles = transform.eulerAngles;
    }
}
