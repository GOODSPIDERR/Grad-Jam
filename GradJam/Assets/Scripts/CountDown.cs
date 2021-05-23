using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    int min, sec, simple;
    float timer = 180;
    Text minutes, seconds;
    public bool race = false;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("" + transform.childCount);
        minutes = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        seconds = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        minutes.text = "3:";
        seconds.text = "00";
        //race = true;
    }

    void Update(){
        if(race){
            timer -= Time.deltaTime;
            simple = Mathf.FloorToInt(timer);
            min = simple / 60;
            sec = simple % 60;
            minutes.text = "" + min + ":";
            if(timer < 0){
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
