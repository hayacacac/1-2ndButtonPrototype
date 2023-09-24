using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class BossHealthChecker : MonoBehaviour
    {
        public GameObject VictoryZone;
        private Health bossHealth;
        void Start(){
            bossHealth = GetComponent<Health>();
            VictoryZone.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if(bossHealth.getCurrentHP() == 0) {
                VictoryZone.SetActive(true);
            }
        }
    }
}