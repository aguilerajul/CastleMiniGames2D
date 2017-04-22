using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    //TODO: Implementar la secci'on de opciones

    public void StartGames()
    {
        GameManager.Instance.StartMiniGames();
    }
}
