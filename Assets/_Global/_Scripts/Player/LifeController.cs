using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LifeController : MonoBehaviour
{
    [SerializeField]
    [Range(1, 6)]
    int _lifes;
    
    public float _currentLifes { get; private set; }
    
    private void Start()
    {
        this._currentLifes = _lifes;
    }

    public int GetTotalLifeCount()
    {
        return _lifes;
    }
    
    public void Damage(int damage)
    {
        if(this._currentLifes > 0)
        {
            this._currentLifes -= damage;  
        }
        
        if(this._currentLifes <= 0)
        {
            Die();
        }
    }

    public void AddLifePoint(int lifeCount)
    {
        this._currentLifes = Mathf.Min(this._currentLifes + lifeCount, this._lifes);
    }


    protected abstract void Die();
}
