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
        
        public override void Perform(PlayerController player){
            //攻撃開始位置のオフセット
            Vector3 offset = new Vector3(0.25f,0,0);
            // 左向きのときの斑点が複数かからないように毎回スピードを初期化
            float speed = attackSpeed;

            //左向きの時は攻撃方向を逆にする
            if (player.getterFlipX()){
                offset *= -1;
                speed *= -1;
            }

            // あたり判定オブジェクト生成
            GameObject closeAttack = Object.Instantiate(closeAttackPrefab, player.transform.position+offset, player.transform.rotation);

            //飛ばす距離を渡す
            CloseAttackController cac = closeAttack.GetComponent<CloseAttackController>();
            cac.range = attackRange;

            // プレイヤーの向いている方に飛ばす
            Rigidbody2D rb = closeAttack.GetComponent<Rigidbody2D>();
            rb.AddForce(player.transform.right * speed, ForceMode2D.Impulse);
        }
    }
}