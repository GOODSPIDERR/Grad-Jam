using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyingClock : MonoBehaviour
{
    float transitionTime = 5f;
    //AudioListener audioListener;
    private AudioSource[] allAudioSources;


    void Start()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        StartCoroutine("Transition");
        //audioListener = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>();
    }

    void Update()
    {
        foreach (AudioSource audioS in allAudioSources)
        {
            if (audioS != null)
                audioS.pitch = Time.timeScale;
        }

        AudioListener.volume -= transitionTime * Time.timeScale * Time.deltaTime;
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(transitionTime * Time.timeScale);
        SceneManager.LoadScene(2);
    }
}
