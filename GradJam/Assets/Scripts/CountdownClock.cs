using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;


public class CountdownClock : MonoBehaviour
{
    //Needs more comments
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
        //Debug.Log("" + transform.childCount);
        //minutes = this.gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        //seconds = this.gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();


        AudioSource[] sounds;
        sounds = GetComponents<AudioSource>();
        alpha = sounds[0];
        beta = sounds[1];

        //race = true;

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

            //timer -= Time.deltaTime;
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
        while (timer >= 0)
        {
            timer--;
            //Sequence textTween = DOTween.Sequence();
            numbers.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            numbers.DOScale(new Vector3(1, 1, 1), 0.25f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(1);
        }
    }
}
