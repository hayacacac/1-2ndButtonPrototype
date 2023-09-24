using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class HalfButtonUIController : MaskSlider
    {
        [SerializeField]
        GameObject keyGauge; //2分の1ボタンUI上のキーの表示

        public override void SetMaskRatio(float ratio)
        {
            base.SetMaskRatio(ratio); // 親クラスのメソッドを呼び出す

            bool isGaugeMax = ratio == 1f;
            keyGauge.SetActive(isGaugeMax);
        }
    }
}