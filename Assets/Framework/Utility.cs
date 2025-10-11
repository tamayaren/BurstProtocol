using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Framework
{
    public class Utility : MonoBehaviour
    {
        public static Vector3 MouseGlobalPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            return Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }
}
