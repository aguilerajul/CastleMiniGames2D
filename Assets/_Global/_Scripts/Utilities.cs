using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static float CalculateDistance(Transform object1, Transform object2)
    {
        return Vector2.Distance(object1.position, object2.position);
    }
}
