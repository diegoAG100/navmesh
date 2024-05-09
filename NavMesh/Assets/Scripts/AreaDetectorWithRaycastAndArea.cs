using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AreaDetectorWithRaycastAndArea : MonoBehaviour
{
    public enum State{gardar,perseguir,stop}
    public float radio = 10;
    public LayerMask playerLayer;
    public AgentePerseguidor perseguidor;
    public Material chaseColor;
    public Material stopColor;
    public float vision=45;
    private Collider[] colliders; 
    private State state = 0;
    private Renderer render;

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        GotoNextPoint();
    }

    void GotoNextPoint() {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }

    void GotoCurrentPoint() {
        agent.destination = points[destPoint].position;

        destPoint = (destPoint - 1) % points.Length;
    }

    private void ResumePatrol(){
        agent.Resume();
        state=State.gardar;
    }

    void Update()
    {
        Movement();
        StateMachine();
        if (!agent.pathPending && agent.remainingDistance < 0.5f){
        GotoNextPoint();
        }
    }

    private void StateMachine(){
        switch(state){
            case State.perseguir:
                if(perseguidor.enabled==false){
                    perseguidor.enabled=true;
                    perseguidor.goal = colliders[0].transform;
                    render.material=chaseColor;
                }
                break;
            case State.gardar:
                if(agent.isStopped==true){
                    GotoCurrentPoint();
                }
                break;
            case State.stop:
                if(perseguidor.enabled==true){
                    perseguidor.enabled=false;
                    perseguidor.goal = null;
                    perseguidor.StopFollow();
                    render.material=stopColor;

                    agent.Stop();
                    Invoke("ResumePatrol",5);
                }
                break;
        }
    }

    private void Movement(){
        colliders = Physics.OverlapSphere(transform.position, radio, playerLayer);

        if(colliders.Length>0){
            RaycastHit hit;
            if(Physics.Raycast(transform.position,colliders[0].transform.position-transform.position,out hit))
            {
                float angle = Vector3.Angle(transform.forward,colliders[0].transform.position-transform.position);
                Debug.Log(angle);
                if(angle<vision){
                    if(hit.collider.tag=="Player"){
                        if(perseguidor.enabled==false){
                            state=State.perseguir;
                        }
                    }
                    else{
                        state=State.stop;
                    }
                }
            }
            else{
                state=State.stop;
            }
        }
        else{
            state=State.stop;
        }
    }

    private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radio);
            RaycastHit hit;

            if(Application.isPlaying==false){
                return;
            }

            if(colliders.Length>0){
                if(Physics.Raycast(transform.position,colliders[0].transform.position-transform.position,out hit)){
                    if(hit.collider.tag=="Player"){
                        Gizmos.color = Color.red;
                    }
                    else{
                        Gizmos.color = Color.green;
                    }
                    Gizmos.DrawRay(transform.position, colliders[0].transform.position-transform.position);
            }
            }

        }

}
