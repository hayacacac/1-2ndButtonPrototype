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
            Guard
        }

        public ActionList actionA;
        public ActionList actionB;
        private ButtonAction myActionA;
        private ButtonAction myActionB;

        private PlayerController player;

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
                    action = new ShootBullet();
                    break;
                case ActionList.Guard:
                    action = new Guard();
                    break;
                default:
                    Debug.LogError("存在しないアクションが指定されました。");
                    action = new NullAction();
                    break;
            }
            return action;
        }
    }

    /// <summary>
    /// アクションの基底クラス
    /// </summary>
    public class ButtonAction
    {
        public virtual void Perform(PlayerController player){}
    }

    /// <summary>
    /// 何もしないアクションのクラス。
    /// 存在しないアクションが指定されたときに、代わりにこれが入る。
    /// </summary>
    public class NullAction : ButtonAction
    {
        public override void Perform(PlayerController player){
            Debug.LogError("NullAction: 適切なアクションを設定してください。");
        }
    }

    /// <summary>
    /// 遠距離攻撃のクラス。弾を飛ばす。
    /// </summary>
    public class ShootBullet : ButtonAction
    {
        public override void Perform(PlayerController player){
            //弾丸生成位置のオフセット
            Vector3 offset = new Vector3(0.5f,0,0);
            //弾丸の射出速度
            float bulletSpeed = 10f;

            //左向きの時は弾の方向を逆にする
            if (player.getterFlipX()){
                offset *= -1;
                bulletSpeed *= -1;
            }

            //弾丸生成と発射
            GameObject bullet = Object.Instantiate(player.bulletPrefab, player.transform.position+offset, player.transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(player.transform.right * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// ガードのクラス。一定時間無敵
    /// </summary>
    public class Guard : ButtonAction
    {
        public override void Perform(PlayerController player){
            //盾生成位置のオフセット
            Vector3 offset = new Vector3(0.0f,-0.05f,0);
            float shieldTimeSec = 3f;
            GameObject shield = Object.Instantiate(player.shieldPrefab, player.transform.position+offset, player.transform.rotation, player.gameObject.transform);

            // shieldTimeSec秒間だけ無敵&盾あり
            Object.Destroy(shield, shieldTimeSec);
            player.Muteki(shieldTimeSec);
        }
    }
}
