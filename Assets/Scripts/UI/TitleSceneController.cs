using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI
{
    [RequireComponent(typeof(SceneTransition))]
    public class TitleSceneController : MonoBehaviour
    {
        private bool isPushed = false;
        public Image fadePanel; // フェード用のUIパネル（Image）
        public float fadeDuration = 1.0f; // フェードの完了にかかる時間

        private SceneTransition sceneTransition;

        void Start(){
            sceneTransition = GetComponent<SceneTransition>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isPushed) sceneTransition.StartTransition(fadePanel, fadeDuration, "SampleScene");
                isPushed = true;
            }
        }
    }
}