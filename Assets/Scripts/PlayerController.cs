using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Animator animator;
    Vector3 startPosition;

    float playerHeight = 1.45f; //1.35 m
    public float jumpForce = 6f;
    public float runningSpeed = 5f; //5m x seg
    float move;

    public LayerMask groundMask;

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";
    const string STATE_SPEED = "speed";

    int healthPoints = 1, manaPoints = 1;
    public const int INITIAL_HEALT = 100, INITIAL_MANA = 15,
        MAX_HEALTH = 200, MAX_MANA = 30,
        MIN_HEALT = 10, MIN_MANA = 0;

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.5f;

    float distanceScore = 0;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        startPosition = new Vector3(-8,-2,0);
    }

    public void StartGame()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        animator.SetFloat(STATE_SPEED, 0f);

        healthPoints = INITIAL_HEALT;
        manaPoints = INITIAL_MANA;

        Invoke("RestartPosition", 0.2f);

    }

    private void RestartPosition()
    {
        this.rigidBody.velocity = Vector2.zero;
        this.transform.position = startPosition;

        //Restart Camera
        GameObject mCamera = GameObject.Find("Main Camera");
        mCamera.GetComponent<CameraFollow>().ResetCameraPosition();

    }

    void Update()
    {
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());
        animator.SetFloat(STATE_SPEED, Mathf.Abs(rigidBody.velocity.x));
    }

    void FixedUpdate()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetButton("Jump"))
            {
                Jump(false);
            }
            if (Input.GetButton("SuperJump"))
            {
                Jump(true);
            }

            move = Input.GetAxis("Horizontal");

            if (move != 0)
            {
                Run();
            }
        }
  
    }

  
    void Jump(bool superJump)
    {
        float jumpForceFactor = jumpForce;

        if (IsTouchingTheGround())
        {
            if (superJump && manaPoints >= SUPERJUMP_COST)
            {
                manaPoints -= SUPERJUMP_COST;
                jumpForceFactor *= SUPERJUMP_FORCE;
            }

            rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);  
        }
    }

    bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(
             this.transform.position,
            Vector2.down,
            playerHeight,
            groundMask)) {

            return true;
        }
        else
        {
            return false;
        }
    }

    void Run()
    {
        rigidBody.velocity = new Vector2(move * runningSpeed, rigidBody.velocity.y);
        
        
        bool flipped = move < 0;
        this.transform.rotation = Quaternion.Euler(new Vector2(0f, flipped ? 180f : 0f));

    }

    public void Die()
    {

        float travelledDistance = GetTravelledDistance();
        float prevoiusMaxDistance = PlayerPrefs.GetFloat("maxscore",0f);

        if(travelledDistance > prevoiusMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }


        this.animator.SetBool(STATE_ALIVE , false);
        GameManager.sharedInstance.GameOver();
    }

    public void CollectHealth (int points)
    {
        this.healthPoints += points;
        if(this.healthPoints >= MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }
    }

    public void CollectMana (int points)
    {
        this.manaPoints += points;
        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        if (this.healthPoints <= 0)
        {
            Die();
        }

        return this.healthPoints;
    }

    public int GetMana()
    {
        return this.manaPoints;
    }

    public float GetTravelledDistance()
    {
        if(distanceScore<(this.transform.position.x - startPosition.x))
        {
            distanceScore = this.transform.position.x - startPosition.x;
        }

        return distanceScore;
    }

}


/* 
 * -----NOTAS-----*
 

Debug.DrawRay(this.transform.position, Vector2.down*playerHeight, Color.red); //Dibuja un rayo o una línea

//GetKey es para mantener pulsado la tecla
//GetKeyDown es para detectar el pulsado
 //MouseButton: 0 - izquierdo, 1 - derecho, 2 - ruedita

 Transform: posición, rotación y escala

*Physics2D.Raycast:
    Utiliza un layer para detectar si el personaje toca el suelo
    Raycast: 
     *  Lanza un rayo para detectar una distancia
     *  Primer parámetro: vector origen desde el centro del personaje
     *  Segundo parámetro: direccion del rayo
     *  Tercer parámetro: distancia
     *  Cuarto parámetro: A qué toca el rayo
 
 
Rotation:
    this.transform.rotation = Quaternion.Euler(new Vector2(0f, flipped ? 180f : 0f));
                                                               Si necesita rotar, rota 180 grados : si no, no rota.

*/
