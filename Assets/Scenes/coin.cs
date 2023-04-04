using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    [SerializeField] AudioClip coinSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
