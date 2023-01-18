using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //=======================================
    private Rigidbody playerRD;
    private GameObject focalPoint;

    private float powerUpStreght = 15.0f;

    public float speed = 5.0f;
    public bool hasPowerUp = false;
    public bool hasHomingMissile = false;


    //=======================================
    public GameObject powerUpIndcator;
    public GameObject powerUpMissile;
    
    Coroutine powerUpTimer;
    

    //=======================================
    public PowerUpType currentPowerUp = PowerUpType.None;
    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;

    //======================================
    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;
    private Coroutine smashingBall;

    bool smashing = false;
    float floorY;

    // Start is called before the first frame update
    void Start()
    {
        playerRD = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        
        
    }

    // Update is called once per frame
    public void Update()
    {
        
        // This is to change to the transform postion of player based on other game objects postion.
        float forwardInput = Input.GetAxis("Vertical");
        playerRD.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerUpIndcator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        powerUpMissile.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }

      

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Power_Up"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            powerUpTimer = StartCoroutine(timer(7));
            powerUpIndcator.gameObject.SetActive(true);
        }
        if (other.CompareTag("Power_Missile"))
        {
            Destroy(other.gameObject);
            hasHomingMissile = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            powerUpMissile.gameObject.SetActive(true);
            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        //How to apply power ups and in the form of force. 

        if(collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRD = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRD.AddForce(awayFromPlayer * powerUpStreght, ForceMode.Impulse);
            Debug.Log("Plyer collided with: " + collision.gameObject.name + " with powerup set to " + currentPowerUp.ToString());
        }
    }
    void LaunchRockets()
    {
        foreach(var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBhaviour>().Fire(enemy.transform);
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
        powerUpIndcator.gameObject.SetActive(false);
        
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasHomingMissile = false;
        currentPowerUp = PowerUpType.None;
        powerUpMissile.gameObject.SetActive(false);
    }

    IEnumerable Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();

        floorY = transform.position.y;
        float jumpTime = Time.time + hangTime;
        while(Time.time < jumpTime)
        {
            playerRD.velocity = new Vector2(playerRD.velocity.x, smashSpeed);
            yield return null;
        }
        while (transform.position.y > floorY)
        {
            playerRD.velocity = new Vector2(playerRD.velocity.x, -smashSpeed * 2);
            yield return null;
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] !=null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }
            smashing = false;
        }
    }
}
