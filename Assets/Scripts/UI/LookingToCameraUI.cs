using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingToCameraUI : MonoBehaviour
{
    private static Camera _camera;

    private void Awake()
    {
        if (_camera is null)
        {
            _camera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        transform.forward = -_camera.transform.forward;
    }
}
