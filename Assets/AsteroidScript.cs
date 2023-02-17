using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;                           
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        Vector3 movementVector = player.transform.position - transform.position;
        //stabilizacja predkosci asteroidy
        movementVector = movementVector.normalized * 10;
        rb.AddForce(movementVector, ForceMode.VelocityChange);
        rb.AddTorque(new Vector3(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90)));

        //rozwiazanie tymczasowe, despawn po opuszczeniu ekranu
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
