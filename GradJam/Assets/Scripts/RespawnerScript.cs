using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class RespawnerScript : MonoBehaviour
{
    //private AssetBundle myBundle;
    //private string[] scenePaths;
    public GameObject alarmClock;

    // Start is called before the first frame update
    void Start()
    {
        //myBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/scenes");
        //scenePaths = myBundle.GetAllScenePaths();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape")){
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        if(transform.position.y < -10){
            transform.position = new Vector3(-26f, 6f, 110f);
        }

        if(distance() < 1){
            SceneManager.LoadScene("", LoadSceneMode.Single);
        }
    }

    double distance(){
        double x, y, z, dist;
        x = transform.position.x - alarmClock.transform.position.x;
        y = transform.position.y - alarmClock.transform.position.y;
        z = transform.position.z - alarmClock.transform.position.z;
        x = x * x;
        y = y * y;
        z = z * z;
        dist = Math.Sqrt(x + y + z);
        return dist;
    }
}