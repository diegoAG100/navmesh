using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetectorWithRaycastAndArea : MonoBehaviour
{

    public float radio = 10;
    public LayerMask playerLayer;
    public AgentePerseguidor perseguidor;
    private Collider[] colliders; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, radio, playerLayer);

        //Aqui hay que hacer un bool que le deje entrar o no dependiendo el estado del centinela
        if(colliders.Length>0){
            RaycastHit hit;
            if(Physics.Raycast(transform.position,colliders[0].transform.position-transform.position,out hit))
            {
                if(Vector3.Dot(transform.position,(colliders[0].transform.position-transform.position).normalized)>0.7f){
                    if(hit.collider.tag=="Player"){
                        if(perseguidor.enabled==false){
                            perseguidor.enabled=true;
                            perseguidor.goal = colliders[0].transform;
                        }
                }
                }
            }

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
