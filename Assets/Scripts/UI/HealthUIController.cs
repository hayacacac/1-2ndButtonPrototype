using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.UI
{
    /// <summary>
    /// HPのUIに関する処理のクラス
    /// </summary>
    public class HealthUIController : MaskSlider
    {
        /// <summary>
        /// 体力に応じて変わるキャラ画像を管理する内部クラス
        /// </summary>
        [System.Serializable]
        class HealthImageObjects
        {
            public GameObject fullHP;
            public GameObject midHP;
            public GameObject lowHP;
            public GameObject zeroHP;

            public void disableAll(){
                fullHP.SetActive(false);
                midHP.SetActive(false);
                lowHP.SetActive(false);
                zeroHP.SetActive(false);
            }
        }

        // 中HPの表示に切り替わる閾値
        float midHpRatio = 0.8f;
        // 低HPの表示に切り替わる閾値
        float lowHpRatio = 0.3f;

        [SerializeField]
        HealthImageObjects healthImageObjects;

        public override void SetMaskRatio(float ratio)
        {
            base.SetMaskRatio(ratio); // 親クラスのメソッドを呼び出す
            
            //一度すべてのキャラ画像を非表示
            healthImageObjects.disableAll();

            // HPの割合に応じたキャラ画像を表示
            if (ratio > midHpRatio){
                // 体力大の表示
                healthImageObjects.fullHP.SetActive(true);
            } else if (ratio >= lowHpRatio){
                // 体力中の表示
                healthImageObjects.midHP.SetActive(true);
            } else if (ratio != 0f){
                // 体力低の表示
                healthImageObjects.lowHP.SetActive(true);
            } else {
                // 体力0の表示
                healthImageObjects.zeroHP.SetActive(true);
            }
        }


    }
}