using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public GameObject motor;

    public int maxHealth = 10;
    public int currentHealth;
    public int currency;

    public GameObject particleEffect;

    public float speed = 150f;
    public float score;
    public bool doubleScore;

    private bool isDead;

    private AudioSource audioSource;
    public AudioClip gettingHitSound;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        audioSource.PlayOneShot(gettingHitSound);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        motor.SetActive(false);
        gameObject.SetActive(false);
        Instantiate(particleEffect, transform.position, Quaternion.identity);
        Instantiate(particleEffect, transform.position + new Vector3(0f, 1f, 1f), Quaternion.identity);
        Instantiate(particleEffect, transform.position + new Vector3(0f, 1f, -1f), Quaternion.identity);
        GameManager.Instance.GameOver();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            // Don't heal
            currentHealth = maxHealth;
        }
        else 
        {
            UIManager.Instance.UpdateHealthUI();
        }
    }

    public void EnableDoublePoints()
    {
        doubleScore = true;

        StartCoroutine(DisablePowerup());
    }

    private IEnumerator DisablePowerup()
    {
        yield return new WaitForSeconds(20.0f);

        doubleScore = false;

    }

}
