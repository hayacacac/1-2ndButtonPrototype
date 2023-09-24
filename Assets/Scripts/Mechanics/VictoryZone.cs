using Platformer.Gameplay;
using UnityEngine;
using System.Collections;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {    
        public GameObject confetti;
        public GameObject billboard;
        private ParticleSystem particleConfetti;
        void Start(){
            particleConfetti = confetti.GetComponent<ParticleSystem>();
        }
        void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null)
            {
                // var ev = Schedule<PlayerEnteredVictoryZone>();
                // ev.victoryZone = this;
                confetti.SetActive(true);
                billboard.SetActive(true);
                StartCoroutine(MoveBillboardAndPlayParticleSystem());
            }
        }
        
        IEnumerator MoveBillboardAndPlayParticleSystem()
        {
            // ビルボードを3秒かけて6fだけ上げる
            Vector3 startPos = billboard.transform.position;
            Vector3 endPos = startPos + new Vector3(0f, 9f, 0f);
            float duration = 3f;
            float startTime = Time.time;

            while (Time.time - startTime < duration)
            {
                float t = (Time.time - startTime) / duration;
                billboard.transform.position = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            }

            // 位置を確定させる（誤差を防ぐため）
            billboard.transform.position = endPos;

            // Particle Systemを再生する
            if (particleConfetti != null)
            {
                particleConfetti.Play();
            }
        }
    }
}