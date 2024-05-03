using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetector : MonoBehaviour
{

    public float radio = 10;
    public LayerMask playerLayer;
    public AgentePerseguidor perseguidor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radio, playerLayer);

        //Aqui hay que hacer un bool que le deje entrar o no dependiendo el estado del centinela
        if(colliders.Length>0){
            if(perseguidor.enabled==false){
                perseguidor.enabled=true;
                perseguidor.goal = colliders[0].transform;
            }
        }
    }

    private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radio);
        }

}
