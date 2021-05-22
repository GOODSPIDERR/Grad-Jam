using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float rotationSpeed = 0.0f;
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0);
    }
}