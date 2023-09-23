using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class EnemyAttack : MonoBehaviour
    {
        public float attackCycle = 2f;
        public float attackRange = 8f;
        private float attackTimer;
        public GameObject bulletPrefab; //敵の攻撃の弾
        public GameObject target; //攻撃対象
        [SerializeField]
        int damage = 1;
        [SerializeField]
        float bulletSpeed = 1f;

        // Start is called before the first frame update
        void Start()
        {
            attackTimer = 0f;
        }

        void Update()
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCycle){
                Attack();
                attackTimer = 0f;
            }
        }

        /// <summary>
        /// 敵の攻撃のクラス。とりあえずプレイヤーに向かって弾を飛ばす。
        /// </summary>
        void Attack()
        {
            //ターゲットまでの距離を取得
            Vector3 targetPosition = target.transform.position;
            float distanceToTarget = Vector3.Distance(targetPosition, this.transform.position);

            if (distanceToTarget <= attackRange){
                //ターゲットの方向を取得
                Vector3 shootDirection = (targetPosition - transform.position).normalized;
                
                // 弾の生成位置のオフセット
                float offset = 0.5f;

                // 弾の生成
                GameObject bullet = Instantiate(bulletPrefab, transform.position+shootDirection*offset, transform.rotation);
                bullet.GetComponent<AttackObject>().damage = damage;

                // ターゲットの方向に弾を飛ばす
                bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
            }

            
        }
    }
}