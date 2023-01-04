using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRD;
    private GameObject focalPoint;
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRD = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {


        // This is to change to the transform postion of player based on other game objects postion.
        float forwardInput = Input.GetAxis("Vertical");
        playerRD.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }
}
