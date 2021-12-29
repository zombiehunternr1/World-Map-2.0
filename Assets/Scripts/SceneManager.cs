using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private bool isFadingBlack;
    private float fadeAmount;
    [SerializeField]
    private float fadeSpeed;
    [SerializeField]
    private Image fadePanel;

    public void SceneTransition()
    {
        StartCoroutine(FadeEffect());
    }

    private void Start()
    {
        StartCoroutine(FadeEffect());
    }

    private IEnumerator FadeEffect()
    {
        Color panelColor = fadePanel.color;
        if (isFadingBlack)
        {
            while(fadePanel.color.a < 1)
            {
                fadeAmount = panelColor.a + (fadeSpeed * Time.deltaTime);
                panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, fadeAmount);
                fadePanel.color = panelColor;
                yield return null;
            }
            Debug.Log("I'm black");
        }
        else
        {
            while(fadePanel.color.a > 0)
            {
                fadeAmount = panelColor.a - (fadeSpeed * Time.deltaTime);
                panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, fadeAmount);
                fadePanel.color = panelColor;
                yield return null;
            }
            Debug.Log("I'm opaque");
        }
    }
}
