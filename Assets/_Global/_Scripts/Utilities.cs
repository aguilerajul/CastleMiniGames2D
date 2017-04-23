using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    public static float CalculateDistance(Transform object1, Transform object2)
    {
        return Vector2.Distance(object1.position, object2.position);
    }

    public static Vector2 LimitPlayerMovementsInScene(Transform currentPlayerPosition, float minPositionWidth, float maxPositionWidth, float minPositionHeight, float maxPositionHeight)
    {
        Vector2 finalPlayerPosition = currentPlayerPosition.transform.position;
        finalPlayerPosition.x = Mathf.Clamp(currentPlayerPosition.transform.position.x, minPositionWidth, maxPositionWidth);
        finalPlayerPosition.y = Mathf.Clamp(currentPlayerPosition.transform.position.y, minPositionHeight, maxPositionHeight);
        return finalPlayerPosition;
    }

    public static string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
