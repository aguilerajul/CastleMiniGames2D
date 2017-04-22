using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField]
    GameObject _prefab;
    [SerializeField]
    float _poolingAmount;
    [SerializeField]
    bool _willGrow;

    List<GameObject> _objectsPoolList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _objectsPoolList = new List<GameObject>();
        for (int i = 0; i < _poolingAmount; i++)
        {
            var prefabInstanace = GameObject.Instantiate(_prefab);
            prefabInstanace.gameObject.SetActive(false);
            _objectsPoolList.Add(prefabInstanace);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _objectsPoolList.Count; i++)
        {
            if (!_objectsPoolList[i].gameObject.activeInHierarchy)
            {
                return _objectsPoolList[i];
            }
        }

        if(_willGrow)
        {
            var prefabInstance = GameObject.Instantiate(_prefab);
            _objectsPoolList.Add(_prefab);
            return prefabInstance;
        }

        return null;
    }
}
