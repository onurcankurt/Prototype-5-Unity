using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.Sockets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton;

    public AudioSource audioSource;
    public Slider volumeSlider;
    private const string volumeLevelKey = "VolumeLevel"; // PlayerPrefs key

    public GameObject titleScreen;

    private float spawnRate = 1.0f;
    public int lives = 3;


    public bool isPauseActive = false;
    public GameObject pausePanel;

    private SC_CursorTrail cursorTrail;

    void Start()
    {
        cursorTrail = GameObject.Find("Main Camera").GetComponent<SC_CursorTrail>();

        livesText.text = "Lives: " + lives;
        audioSource.Play();
        float soundLevel = PlayerPrefs.GetFloat(volumeLevelKey, 0.5f);
        volumeSlider.value = soundLevel;
        audioSource.volume = soundLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isGameActive)
        {
            if (isPauseActive)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }

        if(isPauseActive || !isGameActive)
        {
            cursorTrail.trail.enabled = false;
        }
    }
    
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }  

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true );
        restartButton.gameObject.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }

    public void AdjustVolume()
    {
        audioSource.volume = volumeSlider.value;
        PlayerPrefs.SetFloat(volumeLevelKey, volumeSlider.value);
        PlayerPrefs.Save(); // PlayerPrefs deðiþikliklerini kaydet
    }


    void PauseGame()
    {
        Time.timeScale = 0f; // Zamaný duraklat
        isPauseActive = true;
        audioSource.Pause();

        // Pause panelini aktif hale getir
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }

    void ContinueGame()
    {
        Time.timeScale = 1f; // Zamaný normale çevir
        isPauseActive = false;
        audioSource.Play();

        // Pause panelini deaktif hale getir
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        
    }


}
