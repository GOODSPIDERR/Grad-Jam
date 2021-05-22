using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class DummyScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject enemy;
    public FirstPersonController fpc;
    private bool agro;

    void Start(){
        agro = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Hearing
        if(agro){
            if(fpc.getCrouch() && hearing() > 20 && !spotting()){
                agro = false;
                agent.SetDestination(transform.position);
            } else {
                agent.SetDestination(enemy.transform.position);
            }
        } else {
            if(fpc.getSprint()){
                if(hearing() < 20){
                    agro = true;
                    agent.SetDestination(enemy.transform.position);
                }
            } else if(fpc.getCrouch()) {
                if(hearing() < 5){
                    agro = true;
                    agent.SetDestination(enemy.transform.position);
                }
            } else {
                if(hearing() < 10){
                    agro = true;
                    agent.SetDestination(enemy.transform.position);
                }
            }
            if(spotting() && hearing() < 30){
                agro = true;
                agent.SetDestination(enemy.transform.position);
            }
        }
    }

    double hearing(){
        double x, z, dist;
        x = transform.position.x - enemy.transform.position.x;
        z = transform.position.z - enemy.transform.position.z;
        x = x * x;
        z = z * z;
        dist = Math.Sqrt(x + z);
        return dist;
    }

    bool spotting(){
        Vector3 targetDir = enemy.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, targetDir, out hit, Mathf.Infinity, layerMask)){
            return false;
        }

        if(angle < 60f){
            return true;
        } else {
            return false;
        }
    }
}
