using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;
    Rigidbody2D myRigidbody;
    Player_Movement player;
    float xSpeed;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player_Movement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed,0f);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Enemies")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);    
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }
}