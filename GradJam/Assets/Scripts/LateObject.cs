using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LateObject : MonoBehaviour
{
    AudioSource[] allAudioSources;
    public PillowHit pillowHit;
    public GameObject sceneFade;
    void Start()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        sceneFade.SetActive(true);

        StartCoroutine("SceneTransition");
    }

    void Update()
    {
        foreach (AudioSource audioS in allAudioSources)
        {
            if (audioS != null)
                audioS.pitch = Time.timeScale;
        }

        Time.timeScale -= 0.25f * Time.deltaTime;
    }

    private void LateUpdate()
    {
        pillowHit.canAttack = false;
    }

    IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
