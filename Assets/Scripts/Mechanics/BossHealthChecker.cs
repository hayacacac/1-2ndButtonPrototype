using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Mechanics
{
    public class BossHealthChecker : MonoBehaviour
    {
        public GameObject VictoryZone;
        private Health bossHealth;
        public AudioClip fanfare;
        bool banpei = false;
        public string transitionDestination = "TitleScene";

        void clearTweet(){
            float time = Time.time;
            int m = (int)(time / 60);
            int s = (int)(time % 60);
            naichilab.UnityRoomTweet.Tweet("dojikko-majo-1-2", m+"分"+s+"秒"+"で討伐完了したよ！", "unityroom", "unity1week");
        }

        void trasitionTitle(){
            SceneManager.LoadScene(transitionDestination);
        }

        void Start(){
            bossHealth = GetComponent<Health>();
            VictoryZone.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if(bossHealth.getCurrentHP() == 0) {
                PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
                player.controlEnabled = false;
                if(!banpei){
                    AudioSource audioSource = GameObject.Find("GameController").GetComponent<AudioSource>();
                    audioSource.loop = false;
                    if (audioSource && fanfare)
                        audioSource.clip = fanfare;
                        audioSource.Play();
                    Invoke("clearTweet", 7.0f);
                    Invoke("trasitionTitle", 15.0f);
                    banpei = true;
                }
                VictoryZone.SetActive(true);
            }
        }
    }
}