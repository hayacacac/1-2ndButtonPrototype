using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Platformer.Mechanics
{
    /// <summary>
    /// 2分の1ボタンクラス。
    /// </summary>
    public class HalfButtonController : MonoBehaviour
    {
        // ボタンが有効かどうかのフラグ
        public bool isEnable = true;

        public enum ActionList
        {
            ShootBullet,
            Guard,
            NullAction
        }

        public ActionList actionA;
        public ActionList actionB;
        private ButtonAction myActionA;
        private ButtonAction myActionB;

        private PlayerController player;
        public GameObject ActionManager;

        //確率
        [SerializeField, Range(0.0f, 1.0f)]
        public float AttackAProbability = 0.5f;

        //クールダウン
        private bool isCooldown => cooldownTimer > 0f;
        public float cooldownTime = 5f;
        private float cooldownTimer = 0f;
        public TMP_Text cooldownTextUI;

        // Start is called before the first frame update
        void Start()
        {
            player = GetComponent<PlayerController>();
            myActionA = attachActionInstance(actionA);
            myActionB = attachActionInstance(actionB);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateCooldownTimer();
            
            //Eキーが押されたときに2分の1ボタンの処理
            if (Input.GetKeyDown(KeyCode.E)){
                halfButtonAction();
            }
        }

        // クールダウンの計算
        private void UpdateCooldownTimer(){
            if(isCooldown){
                cooldownTimer = Mathf.Clamp(cooldownTimer-Time.deltaTime, 0, cooldownTime);
                cooldownTextUI.text = "Button: Cooldown " + cooldownTimer.ToString("F3") + "sec";
            } else {
                cooldownTextUI.text = "Button: Available";
            }
        }

        // 2分の1ボタンの処理
        void halfButtonAction(){
            // 無効ならなにもしない
            if(!isEnable) return;

            if (isCooldown == false){
                // 0から1までのランダムな値を生成
                float randomValue = Random.value; 
                
                if (randomValue < AttackAProbability){
                    myActionA.Perform(player);
                } else {
                    myActionB.Perform(player);
                }

                cooldownTimer = cooldownTime;
            }
        }

        // ActionListから指定されたアクションに対応するクラスのインスタンスを返す
        private ButtonAction attachActionInstance(ActionList selectedAction){
            ButtonAction action;
            switch (selectedAction)
            {
                case ActionList.ShootBullet:
                    action = ActionManager.GetComponent<ShootBullet>();
                    break;
                case ActionList.Guard:
                    action = ActionManager.GetComponent<Guard>();
                    break;
                default:
                    Debug.LogError("存在しないアクションが指定されました。");
                    action = ActionManager.GetComponent<NullAction>();
                    break;
            }
            return action ?? ActionManager.GetComponent<NullAction>();
        }
    }
}
