using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Gamesession : MonoBehaviour
{
    [SerializeField] int playerLifes = 3;
    [SerializeField] int playerCoin = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake() 
    {
        int numGameSessions = FindObjectsOfType<Gamesession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }    
    }
    
    void Start() 
    {
        livesText.text = playerLifes.ToString();
        scoreText.text = playerCoin.ToString();    
    }
    // Update is called once per frame
    public void ProcessPlayerDeath()
    {
        if(playerLifes > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }
    public void AddToScore(int pointsToAdd)
    {
        playerCoin += pointsToAdd;
        scoreText.text = playerCoin.ToString(); 
    }

    void ResetGameSession()
    {
        FindObjectOfType<scene_persist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife()
    {
        playerLifes = playerLifes -1;
        int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneIndex);
        livesText.text = playerLifes.ToString(); 
    }
}
