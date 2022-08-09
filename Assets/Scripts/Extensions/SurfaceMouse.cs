using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class SurfaceMouse
{
    private static float _distanceToEmpty = 100f;

    public static Vector3 Position
    {
        get
        {
            var mousePosition = Mouse.current.position.ReadValue();

            var mouseRay = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(mouseRay, out var hitInfo))
            {
                return hitInfo.point;
            }

            return mouseRay.GetPoint(_distanceToEmpty);
        }
    }
}
