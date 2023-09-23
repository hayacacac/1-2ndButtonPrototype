using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// 何かに当たったら消える弾
    /// </summary>
    public class BulletController : AttackObject
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(this.gameObject);            
        }
    }

}

