using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// スクリプトからマスクのサイズを変えて、UIゲージを増減させるクラス
    /// </summary>
    public class MaskSlider : MonoBehaviour
    {
        [SerializeField]
        public RectTransform maskRectTransform;
        [SerializeField]
        float maxMaskSize; //ゲージ最大時のマスクサイズ
        
        /// <summary>
        /// maskのサイズを調整してゲージの量を変える
        /// </summary>
        /// <param name="ratio">ゲージの割合、0 to 1</param>
        public virtual void SetMaskRatio(float ratio){
            Vector2 newSizeDelta = maskRectTransform.sizeDelta;
            newSizeDelta.y = maxMaskSize*ratio;
            maskRectTransform.sizeDelta = newSizeDelta;
        }
    }
}