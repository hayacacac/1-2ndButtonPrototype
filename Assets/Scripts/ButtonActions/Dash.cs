using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    //// <summary>
    /// ダッシュ回避のクラス。ダッシュ中無敵
    /// </summary>
    public class Dash : ButtonAction
    {
        public float dashSpeed = 2f;
        public float dashTime = 0.04f;
        private float dashTimer = 0f;
        
        public enum DashDirectionList
        {
            Forward,
            ForwardUp,
            ForwardDown,
            Up,
            Down,
            Back,
            BackUp,
            BackDown
        }
        public DashDirectionList dashDirection;
        private Vector2 dashDirectionVector = new Vector2(0,0);
        private bool isDashing = false;

        private Vector3 startPosition;

        private PlayerController playerController;

        public override void Perform(PlayerController player){
            dashDirectionVector = GetDirectionVector(dashDirection);

            // キャラのスプライトが反転していたらx成分を反転
            if (player.getterFlipX()){
                dashDirectionVector.x *= -1;
            }

            dashDirectionVector *= (dashSpeed*0.1f);

            // ダッシュ中はコントロール不可
            player.controlEnabled = false;
            
            isDashing = true;
            dashTimer = 0f;
            playerController = player;

            //ダッシュ中は無敵
            player.Muteki(dashTime);
        }

        void Update(){
            // 動かす
            if(isDashing){
                playerController.PerformMovement(dashDirectionVector,true);
                dashTimer += Time.deltaTime;

                // 動き終えたらフラグをリセット
                if(dashTimer >= dashTime){
                    isDashing = false;
                    playerController.controlEnabled = true;
                    dashTimer = 0f;
                }
            }
        }

        //　指定された方向に対応するVector2を返す。正規化されたものが返る。
        Vector2 GetDirectionVector(DashDirectionList dashDirection){ 
            Vector2 directionVector = new Vector2(0,0);
            switch (dashDirection)
            {
                case DashDirectionList.Forward:
                    directionVector = new Vector2(1,0);
                    break;
                case DashDirectionList.ForwardUp:
                    directionVector = new Vector2(1,1);
                    break;
                case DashDirectionList.ForwardDown:
                    directionVector = new Vector2(1,-1);
                    break;
                case DashDirectionList.Up:
                    directionVector = new Vector2(0,1);
                    break;
                case DashDirectionList.Down:
                    directionVector = new Vector2(0,-1);
                    break;
                case DashDirectionList.Back:
                    directionVector = new Vector2(-1,0);
                    break;
                case DashDirectionList.BackUp:
                    directionVector = new Vector2(-1,1);
                    break;
                case DashDirectionList.BackDown:
                    directionVector = new Vector2(-1,-1);
                    break;
                default:
                    Debug.LogError("Dash direction error: 適切なダッシュ回避の方向を指定してください");
                    break;
            }
            Vector2 normalizedVector = directionVector.normalized;
            return normalizedVector;
        }
    }
}