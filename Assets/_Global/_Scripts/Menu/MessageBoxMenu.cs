using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MessageBoxMenu : MonoBehaviour
{
    public const string MESSAGE_BOX_SCENE = "MessageBox";

    [SerializeField]
    Text _gameNameText;

    [SerializeField]
    Text _contentDescriptionText;

    private static string _message;
    private static string _gameName;
    private static float _seconds;
    private static bool _isVisible;
    private float _currentTimeScale;
    
    public static bool IsMessageFinished;

    public static void ShowMessage(string message, float seconds)
    {
        if (_isVisible) return;

        _isVisible = true;

        _message = message;
        _seconds = seconds;
        _gameName = GameManager._currentGameName;

        SceneManager.LoadScene(MESSAGE_BOX_SCENE, LoadSceneMode.Additive);
    }

    void Start()
    {
        _currentTimeScale = Time.timeScale;
        StartCoroutine(ShowMessage_Corrutine(_message, _seconds));
        IsMessageFinished = true;
    }

    IEnumerator ShowMessage_Corrutine(string message, float seconds)
    {
        Time.timeScale = 0.01f;

        _contentDescriptionText.text = string.Empty;
        _gameNameText.text = _gameName;

        float delayPerChar = seconds / message.Length;
        WaitForSeconds wait = new WaitForSeconds(delayPerChar);

        foreach (char c in message)
        {
            _contentDescriptionText.text += c;
            yield return wait;
        }
        
        yield return new WaitForSeconds(2);
        yield return SceneManager.UnloadSceneAsync(MESSAGE_BOX_SCENE);

        Time.timeScale = _currentTimeScale;
    }

    void OnDestroy()
    {
        MessageBoxMenu._isVisible = false;
    }
}
