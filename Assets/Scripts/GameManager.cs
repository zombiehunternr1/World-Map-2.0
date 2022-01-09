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
    private float waitAmount = 1;
    private float fadeAmount;
    private bool allowInteraction;
    private bool isFadingBlack;

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
            int index = selectedLevel.levelData.levelNumber;
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
