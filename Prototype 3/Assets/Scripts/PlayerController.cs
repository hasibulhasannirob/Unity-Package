using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator animator;
    private AudioSource playerAudio;
    
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    //public ParticleSystem dart;

    public AudioClip jumpSound;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    public float floatForce = 50f;
    public float gravityModifier = 1.5f;
    public float maxHeight = 14f;
    private float bottomLimit = 0;
    private float bounceForce = 0.1f;

    public bool onGround = true;
    public bool gameOver = false;

    public Text scoreText;
    private int score = 0;

    void Start()
    {
        
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

        UpdateScore(0);
       
    }

   
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce * Time.deltaTime, ForceMode.Impulse);
            //onGround = false;
            animator.SetTrigger("Jump_trig");
            //dart.Stop();
            playerAudio.PlayOneShot(jumpSound, 0.1f);
        }
        if (transform.position.y > maxHeight)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(Vector3.down * floatForce * Time.deltaTime, ForceMode.Impulse);
        }
        if (transform.position.y <= bottomLimit && !gameOver)
        {
            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        /*if (collision.gameObject.CompareTag("Ground"))
        {
            //onGround = true;
            //dart.Play();

        }*/
        if (collision.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 3.0f);
            Destroy(collision.gameObject);
            UpdateScore(10);
        }
        else if (collision.gameObject.CompareTag("Bomb"))
        {
            gameOver = true;
            //Debug.Log("Game Over!");
            Destroy(collision.gameObject);
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
            
            //dart.Stop();

            if (playerAudio.isPlaying)
            {
                playerAudio.Stop();
            }
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);

            PlayerPrefs.SetInt("FinalScore", score);
            Invoke("LoadGameOverScene", 5.0f);
            //playerAudio.Stop();
        }
    }

    private void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Money: $" + score;
    }
    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
