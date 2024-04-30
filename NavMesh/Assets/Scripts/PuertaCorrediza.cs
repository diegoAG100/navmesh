using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaCorrediza : MonoBehaviour
{
    private Transform target;
    private int position = 1;
    public float velocity = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(movimiento());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.right * velocity * position*Time.deltaTime;
    }

    IEnumerator movimiento(){
        yield return new WaitForSeconds(3f);
        position = position * -1;

        StartCoroutine(movimiento());
    }
}
