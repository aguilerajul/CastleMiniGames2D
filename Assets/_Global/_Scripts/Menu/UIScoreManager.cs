using UnityEngine;
using UnityEngine.UI;

public class UIScoreManager : MonoBehaviour
{
    #region Unity Editor Variables
    [Header("Canvas and Panels")]
    [SerializeField]
    GameObject _canvas;
    [SerializeField]
    GameObject _gameNamePanel;
    [SerializeField]
    GameObject _gameScorePanel;
    [SerializeField]
    GameObject _gameTrophiePanel;
    [Header("Text Font")]
    [SerializeField]
    Font _font;
    [Header("Trophie Sprites")]
    [SerializeField]
    Sprite _woodTrophie;
    [SerializeField]
    Sprite _plateTrophie;
    [SerializeField]
    Sprite _goldTrophie;
    #endregion

    #region Private Variables
    bool _scoreDisplayed;
    #endregion

    #region Unity Methods
    private void OnGUI()
    {
        if (!_scoreDisplayed)
        {
            BindScorePanels();
        }

    }
    #endregion

    #region Custom Methods and Functions
    void BindScorePanels()
    {
        for (int i = 0; i < GameManager.MiniGameStatsPropList.Count; i++)
        {
            try
            {
                MiniGameStats stats = GameManager.MiniGameStatsPropList[i];

                //Set Game Panel Texts
                GameObject gameNameObject = new GameObject("gameName_" + i);
                gameNameObject.transform.position = _gameNamePanel.transform.position;
                gameNameObject = SetTxtAttributes(gameNameObject, stats.GameName);
                gameNameObject.transform.SetParent(_gameNamePanel.transform);
                SetTxtRectTransformAttributes(gameNameObject);

                //Set Score Panel Texts
                GameObject scoreGameObject = new GameObject("gameScore_" + i);
                scoreGameObject.transform.position = _gameScorePanel.transform.position;
                scoreGameObject = SetTxtAttributes(scoreGameObject, "Points: " + stats.Score + " | Difficulty: " + stats.Difficulty);
                scoreGameObject.transform.SetParent(_gameScorePanel.transform);
                SetTxtRectTransformAttributes(scoreGameObject);

                //Set Trhopie Panel Images
                GameObject trophieGameObject = new GameObject("gameTrophie_" + i);
                trophieGameObject.transform.position = _gameTrophiePanel.transform.position;
                trophieGameObject = SetImgAttributes(trophieGameObject, stats.Score);
                trophieGameObject.transform.SetParent(_gameTrophiePanel.transform);
                SetImgRectTransformAttributes(trophieGameObject);

                if (i == GameManager.MiniGameStatsPropList.Count -1)
                {
                    _scoreDisplayed = true;
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }

        }
    }

    private static void SetTxtRectTransformAttributes(GameObject gameObj)
    {
        RectTransform rectT = gameObj.GetComponent<RectTransform>();
        rectT.localScale = new Vector3(1, 1, 1);
        rectT.sizeDelta = new Vector2(100, 10);
    }

    private static void SetImgRectTransformAttributes(GameObject gameObj)
    {
        RectTransform rectT = gameObj.GetComponent<RectTransform>();
        rectT.localScale = new Vector3(1, 1, 1);
        rectT.sizeDelta = new Vector2(25, 25);
    }

    private GameObject SetTxtAttributes(GameObject gameObjTxt, string text)
    {
        //UI Layer
        gameObjTxt.layer = 5;

        Text txt = gameObjTxt.AddComponent<Text>();
        txt.font = _font;
        txt.fontSize = 15;
        txt.color = Color.white;
        txt.text = text;
        txt.horizontalOverflow = HorizontalWrapMode.Overflow;
        txt.verticalOverflow = VerticalWrapMode.Overflow;

        return gameObjTxt;
    }

    private GameObject SetImgAttributes(GameObject gameObjImg, int score)
    {
        //UI Layer
        gameObjImg.layer = 5;

        Image img = gameObjImg.AddComponent<Image>();

        if (score <= 10)
        {
            img.sprite = _woodTrophie;
        }

        if (score > 10 && score <= 20)
        {
            img.sprite = _plateTrophie;
        }

        if (score > 20)
        {
            img.sprite = _goldTrophie;
        }

        return gameObjImg;
    }
    #endregion

}
