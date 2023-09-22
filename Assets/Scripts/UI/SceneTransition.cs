using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

namespace Platformer.UI
{
    /// <summary>
    /// シーン遷移の処理を担うクラス
    /// </summary>
    public class SceneTransition : MonoBehaviour
    {
        public float fadeDuration = 1f;
        public string transitionDestination;

        public void LoadScene(){
            SceneManager.LoadScene(transitionDestination);
        }

        // シーン遷移(フェード)の開始
        public void StartFadeTransition(Image fadePanel){
            StartCoroutine(FadeOutAndLoadScene(fadePanel));
        }

        // フェードアウトしてからシーン遷移
        private IEnumerator FadeOutAndLoadScene(Image fadePanel){
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
            SceneManager.LoadScene(transitionDestination);
        }
    }
}
