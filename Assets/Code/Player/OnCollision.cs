using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    /* PlayerStats playerStats; */
    public Rigidbody sphereRB;

    private float speed;

    public EnemySpawner enemySpawner;

    public LimbManager limbManager;

    private AudioSource audioSource;
    private float lastCollisionTime;
    private float minCollisionInterval = 1.0f;

    public AudioClip collisionSound;
    public AudioClip[] splatterSounds;

    private int counter;


    void Start()
    {
        counter = 0;
        audioSource = GetComponent<AudioSource>();
        Physics.IgnoreLayerCollision(7, 13);
        Physics.IgnoreLayerCollision(8, 13);
    }

    // Update is called once per frame
    void Update()
    {
        speed = sphereRB.velocity.magnitude;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            float timeSinceLastCollision = Time.time - lastCollisionTime;

            if (timeSinceLastCollision >= minCollisionInterval)
            {
                audioSource.pitch = 0.65f;
                if (speed <= 10)
                {
                    audioSource.volume = 0.3f;
                }
                else if (speed > 10 && speed < 30)
                {
                    audioSource.volume = 0.5f;
                }
                else 
                {
                    audioSource.volume = 0.75f;
                }

                audioSource.PlayOneShot(collisionSound);

                // Update the last collision time
                lastCollisionTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && speed > 15f)
        {
            counter++;

            PlayerStats.Instance.currency += 1;
            UIManager.Instance.UpdateCurrencyUI();

            ScoreManager.Instance.AddPoint();
            // Play a random splatter sound
            if (splatterSounds.Length > 0)
            {
                audioSource.pitch = 0.8f;
                int randomIndex = Random.Range(0, splatterSounds.Length);
                audioSource.clip = splatterSounds[randomIndex];
                audioSource.PlayOneShot(audioSource.clip);
            }

            EnemyAILogic enemyAI = collision.gameObject.GetComponent<EnemyAILogic>();


            limbManager.spawnLimbs(enemyAI);
            enemySpawner.DecrementActiveObjectCount();
            Destroy(collision.gameObject);
        }
    }

}
