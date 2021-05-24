using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteScript : MonoBehaviour
{
    //This script self destructs whatever object it's attached to
    public float timer = 5f;

    void Start()
    {
        StartCoroutine("SelfDestruct");
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
