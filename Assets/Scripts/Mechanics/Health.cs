using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 1;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        int currentHP;

        public int getCurrentHP(){
            return currentHP;
        }

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            hpZeroCheck(currentHP);
        }

        /// <summary>
        /// ダメージHPを減らす
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(int damage){
            currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
            hpZeroCheck(currentHP);
        }

        /// <summary>
        /// 現在のHPが0かチェック
        /// </summary>
        /// <param name="currentHP"></param>
        private void hpZeroCheck(int currentHP){
            if (currentHP == 0)
            {
                if(gameObject.CompareTag("Player")){
                    var ev = Schedule<HealthIsZero>();
                    ev.health = this;
                    //Schedule<PlayerDeath>();
                } else if(gameObject.CompareTag("Enemy")){
                    var ev = Schedule<EnemyDeath>();
                    ev.enemy = gameObject.GetComponent<EnemyController>();
                }
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        void Awake()
        {
            currentHP = maxHP;
        }

        // void Update(){
        //     Debug.Log(currentHP);
        //     Debug.Log("IsAlive: " + IsAlive);
        // }
    }
}
