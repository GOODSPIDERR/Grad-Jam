using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BoomerangEffect : MonoBehaviour
{

    bool go;
    GameObject player;
    GameObject pillow;
    public Transform flatPillowToRotate;//the pillow in the prefab
    //PillowRaycast script;
    //Vector3 boomerangOrigin;
    GameObject pillowMesh;
    private AudioSource sound;
    public List<AudioClip> pillowSounds = new List<AudioClip>();


    void Start()
    {
      
        go = false;
        player = GameObject.Find("Player");
        pillow = GameObject.Find("Pillow");
        pillowMesh = GameObject.Find("PillowMesh");
        pillowMesh.GetComponent<SkinnedMeshRenderer>().enabled = false; //make original pillow invisible
        //itemToRotate = gameObject.transform.GetChild(0); //find pillow   
        //script = GetComponent<PillowRaycast>();
        //boomerangOrigin = new Vector3(script.rayOrigin.x, script.rayOrigin.y, script.rayOrigin.z);
        //boomerangOrigin = script.rayOrigin;
        //sound = GetComponent<AudioSource>();
        
        StartCoroutine(Boom());

    }

    IEnumerator Boom()
    {
        //boomerangOrigin = new Vector3(script.rayOrigin.x, script.rayOrigin.y, script.rayOrigin.z);
        go = true;
        yield return new WaitForSeconds(0.5f); //how long to wait till it returns
        go = false;
    }


    // Update is called once per frame
    void Update()
    {
        flatPillowToRotate.transform.Rotate(0, Time.deltaTime * 300, 0); //Rotate The Object

        
        if (go)
        {
                transform.position = Vector3.MoveTowards(transform.position, pillow.transform.position + pillow.transform.forward * 2f, Time.deltaTime * 10); //Change The Position To The Location In Front Of The Player           
                //sound.PlayOneShot(pillowSounds[0]);
        }

        if (!go)
        {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Time.deltaTime * 20); //Return To Player
                //sound.PlayOneShot(pillowSounds[1]);
        }

        if (!go && Vector3.Distance(player.transform.position, transform.position) < 1)
        {

                pillowMesh.GetComponent<SkinnedMeshRenderer>().enabled = true;
                //sound.PlayOneShot(pillowSounds[1]);
                Destroy(this.gameObject);
        }


    }
}