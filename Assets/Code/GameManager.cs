using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverUI;
    public GameObject gameWinUI;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public int currentWave;
    public float waveDuration;
    public float timeIncrement;
    public float timeLeft;

    private float waveTimer; // Timer for the current wave

    public HealthSpawner healthSpawner;
    public EnemySpawner enemySpawner;
    public PowerupSpawner powerupSpawner;
    public EnemyManager enemyManager;
    public GarageManager garageManager;
    public PowerupManager powerupManager;

    private AudioSource audioSource;

    public AudioClip winSound;
    public AudioClip gameOver;
    public AudioClip roundOver;

    public bool inGarage;
    private bool hasWon;
    private bool hasLost; 

    private void UpdateGameState(GameState newState)
    {
        State = newState;
        OnGameStateChanged?.Invoke(newState);
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        /* MainMenu(); */
        audioSource = GetComponent<AudioSource>();
        waveDuration = 60.0f;
        timeLeft = waveDuration;
        inGarage = false;
        hasWon = false;
        StartGame();
    }

    void Update()
    {
        switch (State)
        {
            case GameState.Play:
                healthSpawner.isTimer = true;
                enemySpawner.isTimer = true;
                powerupSpawner.isTimer = true;
                waveTimer += Time.deltaTime;

                if (waveTimer >= waveDuration)
                {  
                    DestroyEnemyAIAndPowerups();
                    EnterGarage();
                }
                break;

            case GameState.Garage:
                break;
            case GameState.Victory:
                if (!hasWon)
                {
                    PlayerWon();
                    hasWon = true;
                }
                break;
            case GameState.Lose:
                break;
            case GameState.MainMenu:
                break;
        }
    }

    public void StartGame()
    {
        Debug.Log("GAME STARTED");
        currentWave = 1;
        timeLeft = waveDuration;
        waveTimer = 0;
        UpdateGameState(GameState.Play);
    }

    public void EnterGarage()
    {
        if (currentWave >= 5) {
            UpdateGameState(GameState.Victory);
        }
        else 
        {
            Debug.Log("ENTER GARAGE");
            audioSource.PlayOneShot(roundOver);
            garageManager.OpenGarage();
            UpdateGameState(GameState.Garage);
        }
    }

    public void ExitGarage()
    {  
        Debug.Log("GARAGE LEFT");
        garageManager.CloseGarage();
        NextWave();
        UpdateGameState(GameState.Play);
    }

    private void NextWave()
    {
        Debug.Log("NEW WAVE");
        currentWave++;
        waveDuration += timeIncrement;
        timeLeft = waveDuration;
        UIManager.Instance.UpdateRoundUI();
        waveTimer = 0;
        UpdateGameState(GameState.Play);
    }

    public void PlayerWon()
    {
        Debug.Log("I WON");
        DestroyEnemyAIAndPowerups();
        audioSource.PlayOneShot(winSound);
        gameWinUI.SetActive(true);
    }

    public void GameOver()
    {
        Debug.Log("I DIED");
        healthSpawner.isTimer = false;
        enemySpawner.isTimer = false;
        powerupSpawner.isTimer = false;

        // Disable car audio
        GameObject car = GameObject.FindGameObjectWithTag("Car");
        AudioSource carAudioSource = car.GetComponent<AudioSource>();
        carAudioSource.enabled = false;


        // Play the sound only once
        if (!hasLost)
        {
            audioSource.PlayOneShot(gameOver);
        }
        hasLost = true;

        UpdateGameState(GameState.Lose);
        gameOverUI.SetActive(true);
    }

    private void DestroyEnemyAIAndPowerups()
    {
        enemyManager.DestroyEnemies();
        enemySpawner.isTimer = false;
                    
        powerupManager.DestroyPowerups();
        powerupSpawner.isTimer = false;
        healthSpawner.isTimer = false;
    }


    /* Menu Functions */

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        Debug.Log("IN MAIN MENU");
        UpdateGameState(GameState.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }
}

public enum GameState
{
    Play,
    Garage,
    Victory,
    Lose, 
    MainMenu
}