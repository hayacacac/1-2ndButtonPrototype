using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class CloseAttackController : MonoBehaviour
    {
        // オブジェクトの初期位置を保存する変数
        private Vector3 initialPosition;
        //移動距離
        public float range = 0;

        // スクリプトの開始時に初期位置を保存
        void Start()
        {
            initialPosition = transform.position;
        }

        void Update(){
            if(Vector3.Distance(initialPosition, transform.position) >= range){
                Destroy(this.gameObject); 
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            // プレイヤーに当たったら無視
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Muteki"))
            {
                return;
            }

            Destroy(this.gameObject); 
        }
    }
}



