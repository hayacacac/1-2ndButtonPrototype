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
        [Header("Button Settings")]
        // ボタンが有効かどうかのフラグ
        public bool isEnable = true;

        // 2分の1ボタンのキー
        [SerializeField]
        private KeyCode buttonKey;

        // ボタン番号。複数置いたときに区別するために使う。
        public int buttonNumber = 0;

        public enum ActionList
        {
            ShootBullet,
            Guard,
            CloseAttack,
            Dash,
            NullAction
        }
        [Header("Action Settings")]
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
        [Header("Cooldown")]
        public float cooldownTime = 5f;
        private float cooldownTimer = 0f;
        [SerializeField]
        private HalfButtonUIController halfButtonUI;

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
            if (player.controlEnabled && Input.GetKeyDown(buttonKey)){
                halfButtonAction();
            }
        }

        // クールダウンの計算
        private void UpdateCooldownTimer(){
            float cooldownRemainingRatio = 0f;
            if(isCooldown){
                cooldownTimer = Mathf.Clamp(cooldownTimer-Time.deltaTime, 0, cooldownTime);

                // クールダウンの経過割合を計算
                cooldownRemainingRatio = Mathf.InverseLerp(0f, cooldownTime, cooldownTimer);
            }

            halfButtonUI.SetMaskRatio(1f-cooldownRemainingRatio);
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
                case ActionList.CloseAttack:
                    action = ActionManager.GetComponent<CloseAttack>();
                    break;
                case ActionList.Dash:
                    action = ActionManager.GetComponent<Dash>();
                    break;
                default:
                    Debug.LogError("NullAction: 適切なアクションを設定してください。");
                    action = ActionManager.GetComponent<NullAction>();
                    break;
            }
            return action ?? ActionManager.GetComponent<NullAction>();
        }
    }
}
