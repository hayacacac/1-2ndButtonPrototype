using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class BossAttack : MonoBehaviour
    {
        // 敵が攻撃し終わってから次の攻撃に入るまで
        public float attackCycle = 2f;
        private float attackTimer;
        public GameObject bossBulletPrefab; //敵の攻撃の弾
        [SerializeField]
        int damage = 1;
        [SerializeField]
        float bulletSpeedStart = 2f;

        private Vector3[] directions = new Vector3[5];

        // Start is called before the first frame update
        void Start()
        {
            attackTimer = 0f;

            directions[0] = new Vector3(-1.0f, 1.0f, 0f).normalized;
            directions[1] = new Vector3(-0.5f, 1.0f, 0f).normalized;
            directions[2] = new Vector3(0.0f, 1.0f, 0f).normalized;
            directions[3] = new Vector3(0.5f, 1.0f, 0f).normalized;
            directions[4] = new Vector3(1.0f, 1.0f, 0f).normalized;
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
            // 弾の生成位置のオフセット
            float offset = 0.5f;

            for (int i = 0; i < directions.Length; i++) {
                // 弾の生成
                GameObject bullet = Instantiate(bossBulletPrefab, transform.position+directions[i]*offset, transform.rotation);
                bullet.GetComponent<AttackObject>().damage = damage;

                // 弾を飛ばす
                bullet.GetComponent<Rigidbody2D>().velocity = directions[i] * bulletSpeedStart;
            }
        }
    }
}
