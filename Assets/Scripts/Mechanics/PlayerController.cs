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

        public GameObject bulletPrefab;
        public GameObject shieldPrefab;

        //クールダウン
        private bool isCooldown => cooldownTimer > 0f;
        public float cooldownTime = 5f;
        private float cooldownTimer = 0f;
        public TMP_Text cooldownTextUI;

        //体力UI
        public TMP_Text HealthTextUI;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
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

            UpdateCooldownTimer();

            UpdateHealthUI();

            //Eキーが押されたときに2分の1ボタンの処理
            if (Input.GetKeyDown(KeyCode.E)){
                ButtonAction();
            }

            base.Update();
        }

        private void ButtonAction(){
            //ボタンの処理
            if (isCooldown == false){
                // 0から1までのランダムな値を生成
                float randomValue = Random.value; 
                
                if (randomValue < 0.5f){
                    Attack();
                } else {
                    Guard();
                }

                cooldownTimer = cooldownTime;
            }
        }

        private void UpdateHealthUI(){
            HealthTextUI.text = "HP: " + health.getCurrentHP().ToString();
        }

        private void UpdateCooldownTimer(){
            if(isCooldown){
                cooldownTimer = Mathf.Clamp(cooldownTimer-Time.deltaTime, 0, cooldownTime);
                cooldownTextUI.text = "Button: Cooldown " + cooldownTimer.ToString("F3") + "sec";
            } else {
                cooldownTextUI.text = "Button: Available";
            }
        }

        private void Attack(){
            //弾丸生成位置のオフセット
            Vector3 offset = new Vector3(0.5f,0,0);
            //弾丸の射出速度
            float bulletSpeed = 10f;

            //左向きの時は弾の方向を逆にする
            if (spriteRenderer.flipX){
                offset *= -1;
                bulletSpeed *= -1;
            }

            //弾丸生成と発射
            GameObject bullet = Instantiate(bulletPrefab, transform.position+offset, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        }

        private void Guard(){
            //盾生成位置のオフセット
            Vector3 offset = new Vector3(0.5f,0,0);
            float shieldTimeSec = 3f;

            //左向きの時は盾の方向を逆にする
            if (spriteRenderer.flipX){
                offset *= -1;
            }
            GameObject shield = Instantiate(shieldPrefab, transform.position+offset, transform.rotation, this.gameObject.transform);
            StartCoroutine(DestroyGuard(shieldTimeSec, shield));
        }

        IEnumerator DestroyGuard(float waitTime, GameObject shield){
            this.gameObject.tag = "Muteki";
            yield return new WaitForSeconds(waitTime);
            Destroy(shield);
            this.gameObject.tag = "Player";
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