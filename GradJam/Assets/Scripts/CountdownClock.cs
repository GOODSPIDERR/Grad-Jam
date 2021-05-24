using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class CountdownClock : MonoBehaviour
{
    int min, sec, simple;
    public float timer = 180;
    TextMeshProUGUI minutes;
    TextMeshProUGUI seconds;
    public bool race = false;
    public bool music = false;
    private AudioSource alpha, beta;

    public Image countdown;
    public float waitTime = 150.0f;



void Start()
    {
        //Debug.Log("" + transform.childCount);
        minutes = this.gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        seconds = this.gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        

        AudioSource[] sounds;
        sounds = GetComponents<AudioSource>();
        alpha = sounds[0];
        beta = sounds[1];

        minutes.text = "2:";
        seconds.text = "30";
        //race = true;
    }
    public void MusicStart()
    {

        alpha.Play();
        beta.Play();
    }
    void Update()
    {

        if (race)
        {
            
            countdown.fillAmount += 1.0f / waitTime * Time.deltaTime;
            
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

            if (timer < 0)
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
            }
            else if (sec < 10)
            {
                seconds.text = "0" + sec;
            }
            else
            {
                seconds.text = "" + sec;
            }
        }
    }
}
