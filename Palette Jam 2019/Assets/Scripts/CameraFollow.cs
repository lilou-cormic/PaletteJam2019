using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _camera.transform.position = new Vector3(transform.position.x + 3, _camera.transform.position.y, _camera.transform.position.z);
    }
}
