using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// UIを振動させるクラス
    /// </summary>
    public class UIVibration : MonoBehaviour
    {
        public float duration = 1.0f;      // 振動の継続時間（秒）
        public float interval = 0.1f;      // 振動の周期（秒）
        public float intensity = 10.0f;    // 振動の強さ

        private Vector3 originalPosition;  // UI要素の元の位置
        private RectTransform rectTransform;
        
        void Start(){
            //StartVibration();
        }

        public void StartVibration()
        {
            // UI要素のRectTransformを取得し、元の位置を記録
            rectTransform = GetComponent<RectTransform>();
            originalPosition = rectTransform.anchoredPosition;

            // 振動を開始
            StartCoroutine(VibrateUI());
        }

        private IEnumerator VibrateUI()
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                // 一定の時間間隔で振動させる
                float xOffset = Random.Range(-intensity, intensity);
                float yOffset = Random.Range(-intensity, intensity);
                Vector3 offset = new Vector3(xOffset, yOffset, 0f);

                rectTransform.anchoredPosition = originalPosition + offset;

                yield return new WaitForSeconds(interval);

                elapsed += interval;
            }

            // 振動が終了したら元の位置に戻す
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}