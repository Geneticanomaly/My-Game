using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private HealthUI healthUI;
    private CurrencyUI currencyUI;
    private RoundUI roundUI;
    private HelperUI helperUI;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        healthUI = GetComponent<HealthUI>();
        currencyUI = GetComponent<CurrencyUI>();
        roundUI = GetComponent<RoundUI>();
        helperUI = GetComponent<HelperUI>();
        
        SetHealthUI();
        SetCurrencyUI(0);
        SetRoundUI(1);
    }

    private void Update()
    {
        // Update the timer

        GameManager.Instance.timeLeft -= Time.deltaTime;
        if (GameManager.Instance.timeLeft <= 0f)
        {
            GameManager.Instance.timeLeft = 0f;
            helperUI.adviceText.text = "Go to the garage";
            if (GameManager.Instance.inGarage)
            {
                helperUI.adviceText.text = "Buy upgrades";
            }
        }
        else {
            // Format the time as minutes and seconds
            int minutes = Mathf.FloorToInt(GameManager.Instance.timeLeft / 60f);
            int seconds = Mathf.FloorToInt(GameManager.Instance.timeLeft % 60f);

            // Update the UI text
            helperUI.adviceText.text = "Survive: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
    }

     private void SetHealthUI()
    {
        healthUI.healthText.text = PlayerStats.Instance.maxHealth.ToString();
    }

    public void UpdateHealthUI()
    {
        if (PlayerStats.Instance.currentHealth >= 0)
        {
            healthUI.healthText.text = PlayerStats.Instance.currentHealth.ToString();
        }
        
    }

    private void SetCurrencyUI(int currencyValue)
    {
        currencyUI.currencyText.text = currencyValue.ToString();
    }

    public void UpdateCurrencyUI()
    {
        currencyUI.currencyText.text = PlayerStats.Instance.currency.ToString();
    }

    private void SetRoundUI(int startingRound)
    {
        roundUI.roundText.text = "Round: " + startingRound.ToString();
    }

    public void UpdateRoundUI()
    {
        if (GameManager.Instance.currentWave < 5)
        {
            roundUI.roundText.text = "Round: " + GameManager.Instance.currentWave.ToString();
        }
        else 
        {
            roundUI.roundText.text = "Last round";
        }
        
    }
}
