﻿using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        public PatrolPath path;
        public AudioClip ouch;
        public AudioClip damageAudio;

        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;

        public Bounds Bounds => _collider.bounds;

        public Health health;

        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            health = GetComponent<Health>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            string collisionTag = collision.gameObject.tag;
            if (collisionTag == "Player"){
                var player = collision.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    var ev = Schedule<PlayerEnemyCollision>();
                    ev.player = player;
                    ev.enemy = this;
                }
            } else if (collisionTag == "Bullet"){
                int attackDamage = collision.gameObject.GetComponent<AttackObject>().damage;
                health.TakeDamage(attackDamage);
                //Schedule<EnemyDeath>().enemy = this;
            }
            
        }

        void OnTriggerEnter2D(Collider2D other){
            // プレイヤーに当たったら無視
            if (other.CompareTag("Bullet"))
            {
                int attackDamage = other.gameObject.GetComponent<AttackObject>().damage;
                health.TakeDamage(attackDamage);
                // Schedule<EnemyDeath>().enemy = this;
            }
        }

        void Update()
        {
            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
        }

    }
}