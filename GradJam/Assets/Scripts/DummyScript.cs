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
    private Rigidbody erb;
    public float hearingRange, spottingRange, spottingAngle, pushForce;
    private bool agro, attack;

    void Start(){
        agro = attack =  false;
        erb = enemy.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agro){
            if(fpc.getCrouch() && hearing() > hearingRange && !spotting()){
                agro = false;
                agent.SetDestination(transform.position);
            } else {
                agent.SetDestination(enemy.transform.position);
                if(hearing() <= 2){
                    attack = true;
                }
            }
        } else {
            if(fpc.getSprint()){
                if(hearing() < (2 * hearingRange)){
                    agro = true;
                    agent.SetDestination(enemy.transform.position);
                }
            } else if(fpc.getCrouch()) {
                if(hearing() < (0.5 * hearingRange)){
                    agro = true;
                    agent.SetDestination(enemy.transform.position);
                }
            } else {
                if(hearing() < hearingRange){
                    agro = true;
                    agent.SetDestination(enemy.transform.position);
                }
            }
            if(spotting() && hearing() < spottingRange){
                agro = true;
                agent.SetDestination(enemy.transform.position);
            }
        }
    }

    void FixedUpdate(){
        if(attack){
            knockback();
            attack = false;
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

        if(angle < spottingAngle){
            return true;
        } else {
            return false;
        }
    }

    void knockback(){
        Vector3 targetDir = enemy.transform.position - transform.position;
        targetDir.y = 0;
        erb.AddForce(targetDir.normalized * pushForce, ForceMode.Impulse);
    }
}