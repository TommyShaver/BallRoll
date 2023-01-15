using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private Rigidbody enemyRD;
    private GameObject player;

    public float speed;

    void Start()
    {
        enemyRD = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRD.AddForce(lookDirection * speed * Time.deltaTime);
        if(transform.position.y <-10)
        {
            Destroy(gameObject);
        }
    }
}
