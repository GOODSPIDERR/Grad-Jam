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
        if(agro){
            if(fpc.getCrouch() && spotting() > 25){
                agro = false;
                agent.SetDestination(transform.position);
            } else {
                agent.SetDestination(enemy.transform.position);
            }
        } else {
            if(fpc.getSprint()){
                if(spotting() < 50){
                    agro = true;
                    agent.SetDestination(enemy.transform.position);
                }
            } else if(fpc.getCrouch()) {
                if(spotting() < 10){
                    agro = true;
                    agent.SetDestination(enemy.transform.position);
                }
            } else {
                if(spotting() < 25){
                    agro = true;
                    agent.SetDestination(enemy.transform.position);
                }
            }
        }
    }

    double spotting(){
        double x, z, dist;
        x = transform.position.x - enemy.transform.position.x;
        z = transform.position.z - enemy.transform.position.z;
        x = x * x;
        z = z * z;
        dist = Math.Sqrt(x + z);
        return dist;
    }
}
