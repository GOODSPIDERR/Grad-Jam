using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    int min, sec, simple;
    public float timer = 180;
    Text minutes, seconds;
    public bool race = false;
    public bool music = false;
    //public AudioSource backgroundMusic;
    //public AudioSource ambientSound;
    private AudioSource alpha, beta;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("" + transform.childCount);
        minutes = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        seconds = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();

        AudioSource[] sounds;
        sounds = GetComponents<AudioSource>();
        alpha = sounds[0];
        beta = sounds[1];

        minutes.text = "3:";
        seconds.text = "00";
        //race = true;
    }
    public void MusicStart()
    {
        
        alpha.Play();
        beta.Play();
    }
    void Update(){

        if(race){

            if (!music)
            {
                    music = true;
                    MusicStart();
                
            }

            timer -= Time.deltaTime;
            simple = Mathf.FloorToInt(timer);
            min = simple / 60;
            sec = simple % 60;
            minutes.text = "" + min + ":";
            //backgroundMusic.Play();
            //ambientSound.Play();
            if (timer < 0){
                /*
                Debug.Log("Game Over");
                timer = 1;
                race = false;
                seconds.text = "00";
                */
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
            } else if(sec < 10) {
                seconds.text = "0" + sec;
            } else {
                seconds.text = "" + sec;
            }            
        }
    }
}
