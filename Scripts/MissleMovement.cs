using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleMovement : MonoBehaviour
{
    public float speed;
    private float zBounds = 50;
    private float xBounds = 50;
    private GameObject enemy;
    private Rigidbody missileRD;
    // Start is called before the first frame update
    void Start()
    {
      missileRD = GetComponent<Rigidbody>();
      enemy = GameObject.Find("Enemy");
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (enemy.transform.position - transform.position).normalized;
        missileRD.AddForce(lookDirection * speed * Time.deltaTime);

        
        if(transform.position.z > zBounds || transform.position.z <-zBounds)
        {
            Destroy(gameObject);
        }
        else if(transform.position.x > xBounds || transform.position.x <-xBounds)
        {
            Destroy(gameObject);
        }
    }
}
