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
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton;

    public GameObject titleScreen;

    private float spawnRate = 1.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


}
