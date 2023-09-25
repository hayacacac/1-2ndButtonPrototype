using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// キャラクターがチェックポイントに到達したらリスポーン地点更新
    /// </summary>
    public class TouchCheckPoint : MonoBehaviour
    {
        [SerializeField]
        private GameController gameController;
        private Transform spawnPoint;
        bool isTouched;
        public AudioClip checkAudio;
        public AudioSource audioSource;

        void Awake()
        {    
            audioSource = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        void Start()
        {
            isTouched = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!isTouched && (other.CompareTag("Player") || other.CompareTag("Muteki")))
            {
                gameController.model.spawnPoint = this.transform;
                audioSource.PlayOneShot(checkAudio);
                isTouched = true;
            }
        }
    }
}