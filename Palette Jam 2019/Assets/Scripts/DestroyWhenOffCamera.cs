using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenOffCamera : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (transform.position.x < _camera.transform.position.x - _camera.orthographicSize * _camera.aspect)
            Destroy(gameObject);
    }
}
