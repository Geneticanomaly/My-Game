using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeCollision : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] splatterSounds;

    private void OnTriggerEnter(Collider collision)
    {
       if (collision.gameObject.CompareTag("Enemy"))
       {
            Debug.Log("Blade killed an enemy");
            PlayerStats.Instance.currency += 1;
            UIManager.Instance.UpdateCurrencyUI();

            ScoreManager.Instance.AddPoint();

            if (splatterSounds.Length > 0)
            {
                audioSource = GetComponent<AudioSource>();
                audioSource.pitch = 0.8f;
                int randomIndex = Random.Range(0, splatterSounds.Length);
                audioSource.clip = splatterSounds[randomIndex];
                audioSource.PlayOneShot(audioSource.clip);
            }

            EnemyAILogic enemyAI = collision.gameObject.GetComponent<EnemyAILogic>();

            LimbManager limbManager = GameObject.FindWithTag("LimbManager").GetComponent<LimbManager>();
            EnemySpawner enemySpawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();

            if (limbManager != null && enemySpawner != null)
            {
                limbManager.spawnLimbs(enemyAI);
                enemySpawner.DecrementActiveObjectCount();
                Destroy(collision.gameObject);
            }
       }
    }
}
