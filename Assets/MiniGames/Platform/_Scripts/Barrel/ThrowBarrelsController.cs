using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBarrelsController : MonoBehaviour
{
    [SerializeField]
    float _timeToThrowBarrels = 5f;
        
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("ThrowBarrels", _timeToThrowBarrels, _timeToThrowBarrels);
    }
        
    void ThrowBarrels()
    {
        GameObject barrelInstance = PoolManager.Instance.GetPooledObject();
        if (barrelInstance == null)
            return;

        Vector2 spawnPosition = this.transform.position;
        spawnPosition.y -= 0.5f;
        barrelInstance.transform.position = spawnPosition;
        barrelInstance.SetActive(true);
    }
}
