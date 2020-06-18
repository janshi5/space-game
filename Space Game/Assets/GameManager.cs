using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 1f;
    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    public static bool generalRed;
    public static bool generalGreen;
    public static float playerLife;
    public static bool powerupHeld;

    private void Start()
    {
        powerupHeld = false;
        generalRed = false;
        generalGreen = false;
        playerLife = 3;
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("RedPlanet").Length == 0)
        {
            CompleteLevel();
        }
    }
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Invoke("Restart", 3f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }
}
