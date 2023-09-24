using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// アクションの基底クラス
    /// </summary>
    public class ButtonAction : MonoBehaviour
    {
        public Animator animator;
        public string animationName;
        public virtual void Perform(PlayerController player){
            if(animator != null) animator.SetBool(animationName, true);
        }
    }
}