using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class CloseAttackController : AttackObject
    {
        // オブジェクトの初期位置を保存する変数
        private Vector3 initialPosition;
        //移動距離
        [System.NonSerialized]
        public float range = 0;
        // Y軸追従するプレイヤー
        [System.NonSerialized]
        public Transform player;
        [System.NonSerialized]
        public float offsetY;

        // スクリプトの開始時に初期位置を保存
        void Start()
        {
            initialPosition = transform.position;
            Destroy(this.gameObject, 0.6f);
        }

        void Update(){
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(currentPos.x, player.position.y+offsetY, currentPos.z);
            if(Vector3.Distance(initialPosition, transform.position) >= range){
                Destroy(this.gameObject);
            }
        }

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



