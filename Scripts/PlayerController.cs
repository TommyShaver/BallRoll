using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRD;
    private GameObject focalPoint;

    private float powerUpStreght = 15.0f;

    public float speed = 5.0f;
    public bool hasPowerUp = false;
    public bool hasHomingMissile = false;

    public GameObject powerUpIndcator;
    public GameObject powerUpMissile;
    public GameObject missilePrefab;

    Coroutine powerUpTimer;

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
        powerUpIndcator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        powerUpMissile.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        if(Input.GetKeyDown(KeyCode.F) && hasHomingMissile == true)
        {
            Instantiate(missilePrefab, transform.position, missilePrefab.transform.rotation);
            Debug.Log("Fire");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Power_Up"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            powerUpTimer = StartCoroutine(timer(7));
            powerUpIndcator.gameObject.SetActive(true);
        }
        if (other.CompareTag("Power_Missile"))
        {
            Destroy(other.gameObject);
            hasHomingMissile = true;
            powerUpTimer = StartCoroutine(timer(7));
            powerUpMissile.gameObject.SetActive(true);
        }
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        //How to apply power ups and in the form of force. 

        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRD = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRD.AddForce(awayFromPlayer * powerUpStreght, ForceMode.Impulse);
            //Debug.Log("Hit " + collision.gameObject.name + " with power up set to " + hasPowerUp);
            
        }
    }

    //The proper way to set this frustaing method up. 
    IEnumerator timer(int countLimit)
    {
        int i = 0;
        while (i < countLimit)
        {
            i++;
            Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Finished");
        hasPowerUp = false;
        hasHomingMissile = false;
        powerUpIndcator.gameObject.SetActive(false);
        powerUpMissile.gameObject.SetActive(false);
    }

}
