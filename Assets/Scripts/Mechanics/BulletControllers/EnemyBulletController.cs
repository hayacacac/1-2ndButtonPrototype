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
        public GameObject collisionEffect;
        [SerializeField]

        void OnTriggerEnter2D(Collider2D other)
        {
            // 敵、弾、Cinemachineのコライダも無視
            if (other.CompareTag("Enemy") || other.CompareTag("Muteki") || other.CompareTag("Bullet") || other.CompareTag("EnemyBullet") || other.CompareTag("Cinemachine"))
            {
                return;
            }

            GameObject effect = Object.Instantiate(collisionEffect, this.transform.position, this.transform.rotation);
            Destroy(effect.gameObject, 1);
            Destroy(this.gameObject); 
        }
    }
}