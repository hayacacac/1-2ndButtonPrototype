using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

namespace Platformer.UI
{
    public class SceneTransition : MonoBehaviour
    {
        public void StartTransition(Image fadePanel,float fadeDuration, string sceneName){
            StartCoroutine(FadeOutAndLoadScene(fadePanel, fadeDuration, sceneName));
        }

        private IEnumerator FadeOutAndLoadScene(Image fadePanel,float fadeDuration, string sceneName){
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
            SceneManager.LoadScene(sceneName);
        }
    }
}
