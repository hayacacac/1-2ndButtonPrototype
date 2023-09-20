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
        public virtual void Perform(PlayerController player){}
    }
}