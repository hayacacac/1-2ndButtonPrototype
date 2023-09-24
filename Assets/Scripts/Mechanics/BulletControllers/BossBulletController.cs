using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// 敵が攻撃に使う弾のクラス
    /// </summary>
    public class BossBulletController : AttackObject
    {
        private Rigidbody2D rb;
        public float slowdownRate = 0.95f;

        void Start(){
            rb = GetComponent<Rigidbody2D>();
        }
        void Update(){

        }

        void FixedUpdate()
        {
            if (rb.velocity.magnitude > 0.01f)
            {
                rb.velocity -= rb.velocity.normalized * slowdownRate * Time.fixedDeltaTime;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // 敵、弾、Cinemachineのコライダも無視
            if (other.CompareTag("Enemy") || other.CompareTag("Muteki") || other.CompareTag("Bullet") || other.CompareTag("EnemyBullet") || other.CompareTag("Cinemachine"))
            {
                return;
            }

            Destroy(this.gameObject); 
        }
    }
}