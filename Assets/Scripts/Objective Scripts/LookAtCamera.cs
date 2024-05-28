using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _camera;
    // Start is called before the first frame update
    void OnEnable()
    {
        _camera = Camera.main;
        transform.LookAt(_camera.transform,Vector3.up);
    }

    // Update is called once per frame
    void Update(){
        transform.LookAt(_camera.transform,Vector3.up);
    }
}