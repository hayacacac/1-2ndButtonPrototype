using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// 何もしないアクションのクラス。
    /// 存在しないアクションが指定されたときに、代わりにこれが入る。
    /// </summary>
    public class NullAction : ButtonAction
    {
        public override void Perform(PlayerController player){
            Debug.LogError("NullAction: 適切なアクションを設定してください。");
        }
    }
}