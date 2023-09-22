using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI
{
    /// <summary>
    /// リスタートボタンの入力を見るクラス
    /// </summary>
    [RequireComponent(typeof(SceneTransition))]
    public class TitleButton : MonoBehaviour
    {
        private SceneTransition sceneTransition;
        private bool isPushed;
        public MetaGameController metaGameController;

        // Start is called before the first frame update
        void Start()
        {
            isPushed = false;
            sceneTransition = GetComponent<SceneTransition>();
        }

        public void LoadTitleScene(){
            //まだ押されてなければ遷移を始める
            if (!isPushed){
                metaGameController.ToggleMainMenu(false);
                sceneTransition.LoadScene();
                isPushed = true;
            }
        }
    }
}