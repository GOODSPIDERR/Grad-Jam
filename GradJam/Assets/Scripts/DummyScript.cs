using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class DummyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject enemy;
    private FirstPersonController fpc;
    private Rigidbody erb;
    // private AudioSource hitSound;
    public float hearingRange, spottingRange, spottingAngle, pushForce;
    private bool agro, attack;
    Animator sheepAnim;

    void Start()
    {
        agro = attack = false;
        agent = gameObject.GetComponent<NavMeshAgent>();
        enemy = GameObject.FindGameObjectsWithTag("Player")[0];
        fpc = enemy.GetComponent<FirstPersonController>();
        erb = enemy.GetComponent<Rigidbody>();
        sheepAnim = GetComponent<Animator>();


        //AudioSource[] sounds;
        //sounds = GetComponents<AudioSource>();
        //hitSound = sounds[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (agro)
        {
            if (fpc.getCrouch() && hearing() > hearingRange && !spotting())
            {
                agro = false;
                sheepAnim.SetBool("agro", false);
                agent.SetDestination(transform.position);
            }
            else
            {
                agent.SetDestination(enemy.transform.position);
                if (hearing() <= 1)
                {
                    attack = true;
                }
            }
        }
        else
        {
            if (fpc.getSprint())
            {
                if (hearing() < (1.5 * hearingRange))
                {
                    agro = true;
                    sheepAnim.SetBool("agro", true);
                    agent.SetDestination(enemy.transform.position);
                }
            }
            else if (fpc.getCrouch())
            {
                if (hearing() < (0.5 * hearingRange))
                {
                    agro = true;
                    sheepAnim.SetBool("agro", true);
                    agent.SetDestination(enemy.transform.position);
                }
            }
            else
            {
                if (hearing() < hearingRange)
                {
                    agro = true;
                    sheepAnim.SetBool("agro", true);
                    agent.SetDestination(enemy.transform.position);
                }
            }
            if (spotting() && hearing() < spottingRange)
            {
                agro = true;
                sheepAnim.SetBool("agro", true);
                agent.SetDestination(enemy.transform.position);
            }
        }
    }

    void FixedUpdate()
    {
        if (attack)
        {
            knockback();
            attack = false;
        }
    }

    double hearing()
    {
        double x, z, dist;
        x = transform.position.x - enemy.transform.position.x;
        z = transform.position.z - enemy.transform.position.z;
        x = x * x;
        z = z * z;
        dist = Math.Sqrt(x + z);
        return dist;
    }

    bool spotting()
    {
        Vector3 targetDir = enemy.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, targetDir, out hit, Mathf.Infinity, layerMask))
        {
            return false;
        }

        if (angle < spottingAngle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void knockback()
    {
        Vector3 targetDir = enemy.transform.position - transform.position;
        targetDir.y = 0;
        erb.AddForce(targetDir.normalized * pushForce, ForceMode.Impulse);
        //hitSound.Play();
    }
}
