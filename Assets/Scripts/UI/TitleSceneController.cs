using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class TitleSceneController : MonoBehaviour
{
    private bool isPushed = false;
    public Image fadePanel; // フェード用のUIパネル（Image）
    public float fadeDuration = 1.0f; // フェードの完了にかかる時間

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPushed) StartCoroutine(FadeOutAndLoadScene());
            isPushed = true;
        }
    }

    private IEnumerator FadeOutAndLoadScene(){
        fadePanel.enabled = true;
        float fadeTimer = 0f; 
        float startAlpha = 0f;
        float endAlpha = 1f;
        Color newColor = fadePanel.color;
    
        // フェードアウト
        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            float t = Mathf.Clamp01(fadeTimer / fadeDuration);
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, t);

            newColor = fadePanel.color;
            newColor.a = currentAlpha;
            fadePanel.color = newColor;

            yield return null;
        }
        newColor = fadePanel.color;
        newColor.a = endAlpha;
        fadePanel.color = newColor;

        // シーンをロード
        SceneManager.LoadScene("SampleScene");
    }
}
