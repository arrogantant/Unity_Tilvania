using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamesession : MonoBehaviour
{
    [SerializeField] int playerLife = 3;
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
    // Update is called once per frame
    public void ProcessPlayerDeath()
    {
        if(playerLife > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife()
    {
        playerLife = playerLife -1;
        int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneIndex);
    }
}
