using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BoomerangEffect : MonoBehaviour
{

    bool go;

    GameObject player;
    GameObject pillow;
    public Camera cam;
    Transform itemToRotate;//the pillow in the prefab

    private void Awake()
    {
        cam = FindObjectOfType<Camera>(); 
        //cam = GameObject.Find("MainCamera").GetComponent<Camera>();




    }

    void Start()
    {
      
        go = false;

        player = GameObject.Find("Player");
        pillow = GameObject.Find("Pillow");

        pillow.GetComponent<MeshRenderer>().enabled = false; //make original pillow invisible

        itemToRotate = gameObject.transform.GetChild(0); //find pillow   

        
   
        StartCoroutine(Boom());
    }

    IEnumerator Boom()
    {
        go = true;
        yield return new WaitForSeconds(0.5f); //how long to wait till it returns
        go = false;
    }


    // Update is called once per frame
    void Update()
    {
        itemToRotate.transform.Rotate(0, Time.deltaTime * 300, 0); //Rotate The Object
        Vector3 boomerangOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        if (go)
            {
                transform.position = Vector3.MoveTowards(transform.position, boomerangOrigin + player.transform.forward * 3f, Time.deltaTime * 20); //Change The Position To The Location In Front Of The Player           
            }

            if (!go)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Time.deltaTime * 20); //Return To Player
            }

            if (!go && Vector3.Distance(player.transform.position, transform.position) < 1)
            {

                pillow.GetComponent<MeshRenderer>().enabled = true;
                Destroy(this.gameObject);
            }


    }
}