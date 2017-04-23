using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject _GamePanel;
    [SerializeField]
    GameObject _AudioPanel;
    [SerializeField]
    Scrollbar _volumenSlider;

    public void StartGames()
    {
        GameManager.Instance.StartMiniGames();
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnVolumenChange()
    {
        AudioListener.volume = _volumenSlider.value;
    }

    public void InternalBackMenu()
    {
        _GamePanel.SetActive(true);
        _AudioPanel.SetActive(false);
    }

    public void InternalOptionMenu()
    {
        _GamePanel.SetActive(false);
        _AudioPanel.SetActive(true);
    }
}
