using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        public TMP_Text healthTextUI;
        private bool isMuteki;
        private float mutekiCycle = 0.5f; //無敵時の点滅周期
        private float mutekiTimer = 0f;
        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            isMuteki = false;
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();

            if(isMuteki){
                mutekiTimer += Time.deltaTime;
                // 周期cycleで繰り返す値を取得
                var repeatValue = Mathf.Repeat(mutekiTimer, mutekiCycle);
                // 点滅
                spriteRenderer.enabled = repeatValue >= mutekiCycle * 0.5f;
            } else if(isMuteki == false && spriteRenderer.enabled == false){
                spriteRenderer.enabled = true;
            }

            UpdateHealthUI();

            base.Update();
        }

        //無敵状態にする
        public void Muteki(float mutekiTimeSec){
            StartCoroutine(MutekiCoroutine(mutekiTimeSec));
        }
        
        //無敵状態にする時間を制御するコルーチン
        IEnumerator MutekiCoroutine(float mutekiTimeSec){
            isMuteki = true;
            this.gameObject.tag = "Muteki";
            yield return new WaitForSeconds(mutekiTimeSec);
            this.gameObject.tag = "Player";
            isMuteki = false;
        }

        private void UpdateHealthUI(){
            healthTextUI.text = "HP: " + health.getCurrentHP().ToString();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public bool getterFlipX(){
            return spriteRenderer.flipX;
        }

        void OnCollisionEnter2D(Collision2D collision){
            string collisionTag = collision.gameObject.tag;

            // 敵の弾に当たったらダメージをくらう
            if (collisionTag == "EnemyBullet"){
                var enemyAttack = collision.gameObject.GetComponent<AttackObject>();
                health.TakeDamage(enemyAttack.damage);
                Muteki(3f);
            }
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}