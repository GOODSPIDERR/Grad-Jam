using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;


public class CountdownClock : MonoBehaviour
{
    //This is the timer UI element that is enabled when the first door is destroyed.
    //It includes the music start and also scene reload on timer end. 
    int min, sec, simple;
    public float timer = 180;
    public TextMeshProUGUI timeText;
    public bool race = true;
    public bool music = true;
    private AudioSource alpha, beta;
    public Image countdown;
    public float waitTime = 150.0f;
    public Transform numbers;


    void Start()
    {
        AudioSource[] sounds;
        sounds = GetComponents<AudioSource>();
        alpha = sounds[0];
        beta = sounds[1];
        StartCoroutine("CountDown");
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

            simple = Mathf.FloorToInt(timer);
            min = simple / 60;
            sec = simple % 60;
            timeText.text = "" + min + ":" + sec;

            if (timer < 0)
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
            }

            if (sec < 10 && min > 0)
            {
                timeText.text = "" + min + ":0" + sec;
            }

            else if (sec < 10 && min <= 0)
            {
                timeText.text = "0:0" + sec;
            }
        }
    }

    IEnumerator CountDown()
    {
        while (timer >= 0 && race)
        {
            timer--;
            numbers.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            numbers.DOScale(new Vector3(1, 1, 1), 0.25f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(1);
        }
    }
}
