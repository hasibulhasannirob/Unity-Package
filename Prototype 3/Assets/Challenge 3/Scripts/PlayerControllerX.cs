using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;


    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;
    public float maxHeight = 30.0f;
    private float bounceForce = 0.1f;
    private float bottomLimit = 0;

    public Text scoreText;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

        UpdateScore(0);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && transform.position.y <= maxHeight)
        {
            playerRb.AddForce(Vector3.up * floatForce * Time.deltaTime, ForceMode.Impulse);

        }
        if (transform.position.y > maxHeight)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(Vector3.down * floatForce * Time.deltaTime, ForceMode.Impulse);
        }

        if (transform.position.y <= bottomLimit && !gameOver)
        {
            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceSound, 0.1f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Money"))
        {

            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 3.0f);
            Destroy(other.gameObject);

            UpdateScore(10);

        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Bomb"))
        {

            gameOver = true;
            //Debug.Log("Game Over!");
            Destroy(other.gameObject);
            if (playerAudio.isPlaying)
            {
                playerAudio.Stop();
            }
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);

            PlayerPrefs.SetInt("FinalScore", score);
            Invoke("LoadGameOverScene", 2.0f);

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
