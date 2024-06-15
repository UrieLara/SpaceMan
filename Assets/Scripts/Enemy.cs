using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;

    Rigidbody2D rigidBody;
    

    public bool facingRight = false;
    Vector3 startPosition;

    AudioSource audioSource;
    public AudioClip damageSound;
    public AudioSource enemySound;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        startPosition = this.transform.position;
    }
    void Start()
    {
       // this.transform.position = startPosition;
    }
    // Actualiza el motor de físicas pero en menos frames
    //Para fuerzas, aceleraciones o velocidades
    private void FixedUpdate()
    {
        float currentSpeed = speed;

        if (facingRight)
        {
            //Mirando a la derecha
            currentSpeed = speed;
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            //Mirando a la izquierda
            currentSpeed = -speed;
            this.transform.eulerAngles = Vector3.zero;
        }

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            rigidBody.velocity = new Vector2(currentSpeed, rigidBody.velocity.y);
            enemySound.Play();
            
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
            enemySound.Stop();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Coin")
        {
            return;
        }
        else if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().CollectHealth(-damage);
            audioSource.PlayOneShot(damageSound);

            return;
        }
      
         facingRight = !facingRight;

    }
}
