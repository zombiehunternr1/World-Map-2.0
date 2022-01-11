using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager sceneManagerInstance;
    
    [SerializeField]
    private PathManager pathManager;
    [SerializeField]
    private float fadeSpeed;
    [SerializeField]
    private Image fadePanel;
    [SerializeField]
    private RectTransform levelEnterContainer;
    [SerializeField]
    private RectTransform levelInfoContainer;
    private float waitAmount = 1;
    private float fadeAmount;
    private bool allowInteraction;
    private bool isFadingBlack;
    private int levelIndex;

    private void OnEnable()
    {
        if (sceneManagerInstance == null)
        {
            sceneManagerInstance = this;
            DontDestroyOnLoad(sceneManagerInstance);
        }
        StartCoroutine(FadeEffect(isFadingBlack, null));
    }

    public void SceneTransition(bool isFadingBlack, LevelNodeData selectedLevel)
    {
        StartCoroutine(FadeEffect(isFadingBlack, selectedLevel));
    }

    public void ToggleEnterLevelInfo(bool toggle)
    {
        levelEnterContainer.gameObject.SetActive(toggle);
    }

    private int SetLevelIndex(int levelNumber)
    {
        if(levelNumber != 0)
        {
            return levelNumber;
        }
        else
        {
            return 0;
        }
    }

    private void CheckLevelIndex(LevelNodeData selectedLevel)
    {
        if (selectedLevel != null)
        {
            levelIndex = selectedLevel.levelData.levelNumber;
        }
        else
        {
            levelIndex = 0;
        }
        if(SetLevelIndex(levelIndex) == 0)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            levelInfoContainer.gameObject.SetActive(false);
            SceneManager.LoadScene(levelIndex);
        }
    }

    private IEnumerator FadeEffect(bool isFadingBlack, LevelNodeData selectedLevel)
    {
        yield return new WaitForSeconds(waitAmount);
        Color panelColor = new Color();
        if (isFadingBlack)
        {
            panelColor.a = 0;
            allowInteraction = false;
            while (fadePanel.color.a < 1)
            {
                fadeAmount = panelColor.a + (fadeSpeed * Time.deltaTime);
                panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, fadeAmount);
                fadePanel.color = panelColor;
                yield return fadePanel.color.a;
            }
            yield return new WaitForSeconds(waitAmount);
            ToggleEnterLevelInfo(allowInteraction);
            CheckLevelIndex(selectedLevel);
            StartCoroutine(FadeEffect(allowInteraction, selectedLevel));
        }
        else
        {
            panelColor.a = 1;
            while (fadePanel.color.a > 0)
            {
                fadeAmount = panelColor.a - (fadeSpeed * Time.deltaTime);
                panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, fadeAmount);
                fadePanel.color = panelColor;
                yield return fadePanel.color.a;
            }
            yield return new WaitForSeconds(waitAmount);
            pathManager.CheckUnlockingMovement();
        }
    }
}
