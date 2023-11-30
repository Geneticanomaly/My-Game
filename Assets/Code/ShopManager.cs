using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    
    public GameObject shopOverlay;

    private AudioSource audioSource;
    public AudioClip buySound;

    public bool turretActive = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        shopOverlay.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void showhidePanel(){
        shopOverlay.gameObject.SetActive(!shopOverlay.gameObject.activeSelf);
    }

    public void BuyHealth()
    {
        if (PlayerStats.Instance.currency >= 10)
        {
            if (PlayerStats.Instance.currentHealth < 10)
            {
                Debug.Log("Health bought successfully");
                PlayerStats.Instance.currency -= 10;
                PlayerStats.Instance.Heal(1);
                UIManager.Instance.UpdateHealthUI();
                UIManager.Instance.UpdateCurrencyUI();
                audioSource.PlayOneShot(buySound);
                
            }
            
        }
    }

    public void BuyTurret()
    {
        if (PlayerStats.Instance.currency >= 100)
        {
            if (!turretActive)
            {
                PlayerStats.Instance.currency -= 100;
                TurretManager.Instance.SpawnTurret();
                UIManager.Instance.UpdateCurrencyUI();
                audioSource.PlayOneShot(buySound);
            }
            
        }
        turretActive = true;
    }

    public void BuyBlades()
    {
        if (PlayerStats.Instance.currency >= 300)
        {
            PlayerStats.Instance.currency -= 300;
            BladeManager.Instance.SpawnBlades();
            UIManager.Instance.UpdateCurrencyUI();
            audioSource.PlayOneShot(buySound);
        }
    }
}
