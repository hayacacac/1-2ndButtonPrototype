using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// プレイヤーが攻撃に使う弾のクラス
    /// </summary>
    public class BulletController : AttackObject
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            // プレイヤーに当たったら無視、Cinemachineのコライダも無視
            if (other.CompareTag("Player") || other.CompareTag("Muteki") || other.CompareTag("Bullet") || other.CompareTag("EnemyBullet") || other.CompareTag("Cinemachine"))
            {
                return;
            }

            Destroy(this.gameObject); 
        }
    }
}