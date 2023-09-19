using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class BulletController : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(this.gameObject);            
        }
    }

}

