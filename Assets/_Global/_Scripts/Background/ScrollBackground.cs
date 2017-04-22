using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField]
    float _speed;
    [SerializeField]
    bool _movingInX;

    Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offSet = new Vector2(0, Time.time * _speed);
        if (_movingInX)
        {
            offSet = new Vector2(Time.time * _speed, 0);
        }

        _renderer.material.mainTextureOffset = offSet;
    }
}
