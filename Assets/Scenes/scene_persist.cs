using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene_persist : MonoBehaviour
{
   void Awake() 
    {
        int ScenePersists = FindObjectsOfType<scene_persist>().Length;
        if(ScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }    
    }
    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
