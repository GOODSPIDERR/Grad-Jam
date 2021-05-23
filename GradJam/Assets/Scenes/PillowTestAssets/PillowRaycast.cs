using UnityEngine;
using System.Collections;

public class PillowRaycast : MonoBehaviour
{

    public int pillowDamage = 1;   // Set the number of damage it will take away from the sheep
    public float throwRate = 1f;  // Number in seconds which controls how often the player can fire
    public float pillowRange = 50f;                                        
    public float hitForce = 200f;                     
    private Camera cam;                 
    private float nextThrowDelay;   // Float to store the time the player will be allowed to throw again, after throwing
    public GameObject pillowPrefab;
    public Vector3 rayOrigin;

    void Start()
    {
        
        cam = GetComponentInParent<Camera>();
    }

 
    void Update()
    {
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if (Input.GetMouseButtonDown(1) && Time.time > nextThrowDelay)
        {
           
            // Update the time when our player can fire next
            nextThrowDelay = Time.time + throwRate;
            GameObject clone;
            clone = Instantiate(pillowPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
            
            

            // Create a vector at the center of our camera's viewport
            //Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, pillowRange))
            {

                SheepHitter health = hit.collider.GetComponent<SheepHitter>();
                if (health != null)
                {
                    health.Damage(pillowDamage);
                }

                if (hit.rigidbody != null)
                {
                    
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                    
                }
            }
            
        }
    }



}