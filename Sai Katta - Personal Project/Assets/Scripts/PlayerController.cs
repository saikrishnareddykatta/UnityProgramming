using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private Animator playerAnim;
    public float speed = 10.0f;
    public float xRange = 8.0f;
    private Rigidbody playerRb;
    private float jumpForce = 700.0f;
    private float gravityModifier = -14.7f;
    public bool isOnGround = true;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3 (0, gravityModifier, 0);
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpPlayer();
        if (isOnGround)
        {
            MovePlayerWithConstraints();
        }
       
    }

    // Jumps the Player in Y-Axis
    void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            //playerRb.constraints = RigidbodyConstraints.FreezePositionX;
            playerRb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            Debug.Log("Physics Gravity Modifier" + Physics.gravity);
        }
    }

    // Moves Player on X-Axis with some constraints 
    void MovePlayerWithConstraints()
    {
        if(!gameOver)
        {
            if (transform.position.x < -xRange)
            {
                transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            }

            if (transform.position.x > xRange)
            {
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            }
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            explosionParticle.Play();          
            gameOver = true;
            Debug.Log("Player has collided with enemy and GameOver !!!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            playerAudio.PlayOneShot(crashSound, 1.0f);
            dirtParticle.Stop();
            gameManager.GameOver();
        }
    }

   
}
