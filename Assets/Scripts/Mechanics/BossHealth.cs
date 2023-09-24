using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class BossHealth : Health
    {
        public GamaObjact 
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(BOSS.GetComponentInParent<Health>() == 0){

            }
        }
    }
}