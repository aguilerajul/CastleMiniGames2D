using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeUI : MonoBehaviour
{
    [SerializeField]
    Image _heartPrefab;

    PlayerLife _playerLife;
    Canvas _playerLifeCanvas;
    bool _isHeartCreationFinish;
            
    void Awake()
    {
        _playerLife = FindObjectOfType<PlayerLife>();
        _playerLifeCanvas = GetComponentInChildren<Canvas>();
    }

    private void Start()
    {
        CreateHeartsByTotalLifeCount();
    }

    void Update()
    {
        if(_isHeartCreationFinish)
        {
            for (int i = 0; i < _playerLife.GetTotalLifeCount(); i++)
            {
                if (i < _playerLife._currentLifes)
                {
                    var hearts = _playerLifeCanvas.GetComponentsInChildren<Image>();
                    hearts[i].enabled = true;
                }
                else
                {
                    var hearts = _playerLifeCanvas.GetComponentsInChildren<Image>();
                    hearts[i].enabled = false;
                }
            }
        }        
    }

    private void CreateHeartsByTotalLifeCount()
    {
        if (_heartPrefab != null)
        {
            for (int i = 0; i < _playerLife.GetTotalLifeCount(); i++)
            {
                var heartsInstance = Instantiate(_heartPrefab, _playerLifeCanvas.transform, false);
                heartsInstance.transform.SetParent(_playerLifeCanvas.transform);
            }

            _isHeartCreationFinish = true;
        }
        else
        {            
            Debug.Log("You need to add a Heart Preafab");
        }
    }

}
