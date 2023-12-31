using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    //// <summary>
    /// 近接攻撃のクラス。当たり判定のあるオブジェクトを前に飛ばす
    /// </summary>
    public class CloseAttack : ButtonAction
    {
        public GameObject closeAttackPrefab;
        [SerializeField]
        float attackSpeed = 3f;
        [SerializeField]
        float attackRange = 0.5f;
        [SerializeField]
        int damage = 1;
        public Vector3 offsetY = new Vector3(0,-0.7f,0);
        
        public override void Perform(PlayerController player){
            base.Perform(player);

            //攻撃開始位置のオフセット
            Vector3 offsetX = new Vector3(0.25f,0,0);
            // 左向きのときの斑点が複数かからないように毎回スピードを初期化
            float speed = attackSpeed;

            //左向きの時は攻撃方向を逆にする
            if (player.getterFlipX()){
                offsetX = offsetX * (-1) - new Vector3(0.25f,0,0);
                speed *= -1;
            }

            // あたり判定オブジェクト生成
            GameObject closeAttack = Object.Instantiate(closeAttackPrefab, player.transform.position+offsetX+offsetY, player.transform.rotation);
            closeAttack.GetComponent<AttackObject>().damage = damage;

            //飛ばす距離を渡す
            CloseAttackController cac = closeAttack.GetComponent<CloseAttackController>();
            cac.range = attackRange;
            cac.player = player.gameObject.transform;
            cac.offsetY = offsetY.y;

            // プレイヤーの向いている方に飛ばす
            Rigidbody2D rb = closeAttack.GetComponent<Rigidbody2D>();
            rb.AddForce(player.transform.right * speed, ForceMode2D.Impulse);
        }
    }
}