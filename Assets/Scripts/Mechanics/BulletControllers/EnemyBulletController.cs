using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// 敵が攻撃に使う弾のクラス
    /// </summary>
    public class EnemyBulletController : AttackObject
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            // 敵、弾、Cinemachineのコライダも無視
            if (other.CompareTag("Enemy") || other.CompareTag("Bullet") || other.CompareTag("EnemyBullet") || other.CompareTag("Cinemachine"))
            {
                return;
            }

            Destroy(this.gameObject); 
        }
    }
}