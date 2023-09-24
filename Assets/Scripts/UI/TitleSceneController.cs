using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI
{
    /// <summary>
    /// タイトルシーンからゲームシーンへ遷移する入力を見るクラス
    /// </summary>
    [RequireComponent(typeof(SceneTransition))]
    public class TitleSceneController : MonoBehaviour
    {
        private bool isPushed;
        public Image fadePanel; // フェード用のUIパネル（Image）
        public string transitionDestination = "SampleScene"; //遷移先

        private SceneTransition sceneTransition;

        void Start(){
            sceneTransition = GetComponent<SceneTransition>();
            isPushed = false;
        }

        void Update()
        {
            if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2) && !isPushed)
            {
                sceneTransition.StartFadeTransition(fadePanel);
                isPushed = true;
            }
        }
    }
}