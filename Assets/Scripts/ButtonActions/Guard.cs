using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    //// <summary>
    /// ガードのクラス。一定時間無敵
    /// </summary>
    public class Guard : ButtonAction
    {
        public GameObject shieldPrefab;
        [SerializeField]
        float shieldTimeSec = 3f;
        
        public override void Perform(PlayerController player){
            //盾生成位置のオフセット
            Vector3 offset = new Vector3(0.0f,-0.05f,0);
            GameObject shield = Object.Instantiate(shieldPrefab, player.transform.position+offset, player.transform.rotation, player.gameObject.transform);

            // shieldTimeSec秒間だけ無敵&盾あり
            Object.Destroy(shield, shieldTimeSec);
            player.Muteki(shieldTimeSec);
        }
    }
}