using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// 遠距離攻撃のクラス。弾を飛ばす。
    /// </summary>
    public class ShootBullet : ButtonAction
    {
        public GameObject bulletPrefab;
        [SerializeField]
        float bulletSpeed = 10f; //弾丸の射出速度

        public override void Perform(PlayerController player){
            //弾丸生成位置のオフセット
            Vector3 offset = new Vector3(0.5f,0,0);
            

            //左向きの時は弾の方向を逆にする
            if (player.getterFlipX()){
                offset *= -1;
                bulletSpeed *= -1;
            }

            //弾丸生成と発射
            GameObject bullet = Object.Instantiate(bulletPrefab, player.transform.position+offset, player.transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(player.transform.right * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}