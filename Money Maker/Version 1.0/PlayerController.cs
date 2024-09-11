using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator animator;
    private AudioSource playerAudio;
    
    public ParticleSystem hitByObs;
    public ParticleSystem dart;
    public AudioClip jumpSound;
    public AudioClip crushedSound;
    public float jumpingSpeed = 10;
    public float gravityModifier;
    public bool onGround = true;
    public bool gameOver = false;

    void Start()
    {
        
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

       
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpingSpeed, ForceMode.Impulse);
            onGround = false;
            animator.SetTrigger("Jump_trig");
            dart.Stop();
            playerAudio.PlayOneShot(jumpSound, 2.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            dart.Play();

        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
            hitByObs.Play();
            dart.Stop();

            if (playerAudio.isPlaying)
            {
                playerAudio.Stop();
            }

            playerAudio.PlayOneShot(crushedSound, 1.0f);

            
            //playerAudio.Stop();
        }
    }
}
