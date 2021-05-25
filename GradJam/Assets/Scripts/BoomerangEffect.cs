using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangEffect : MonoBehaviour
{
    private bool go;
    private GameObject player;
    private GameObject pillowMesh;
    private Transform flatPillowToRotate;
    public float throwRange = 10f, throwSpeed = 20f;

    void Start()
    {
        go = false;
        player = GameObject.Find("Player"); //reference to Player prefab
        pillowMesh = GameObject.Find("PillowMesh"); //reference to the instantiated flat pillow
        flatPillowToRotate = gameObject.transform.GetChild(0);
        pillowMesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
        StartCoroutine(Boom());
    }

    IEnumerator Boom()
    {
        //called when flat pillow is instantiated. Starts "go", where it flies away, waits 0.5 seconds and switches the bool 
        //to start the pillow return. 
        go = true;
        yield return new WaitForSeconds(0.5f); //how long to wait till it returns. If you change this here, you also must change 
        //the WaitaSecond Coroutine in PillowHit
        go = false;
    }


    void Update()
    {
        flatPillowToRotate.transform.Rotate(0, Time.deltaTime * 300, 0); //Rotate the pillow and by how quickly
        if (go)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + player.transform.forward * throwRange, Time.deltaTime * throwSpeed); 
            //move the pillow in the direction in front of the player       
        }
        if (!go)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Time.deltaTime * 20); 
            //bring the pillow back
        }
        if (!go && Vector3.Distance(player.transform.position, transform.position) < 1)
        {
            pillowMesh.GetComponent<SkinnedMeshRenderer>().enabled = true; //once it's close, turn the held pillow back on. 
            Destroy(this.gameObject);
        }
    }
}