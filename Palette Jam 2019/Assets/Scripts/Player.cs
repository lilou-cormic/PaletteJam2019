using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    private Transform GroundChecker = null;

    [SerializeField]
    private LayerMask GroundLayer = 0;

    [SerializeField]
    private AudioSource JumpAudioSource = null;

    [SerializeField]
    private AudioSource LandAudioSource = null;

    [SerializeField]
    private AudioSource GameOverAudioSource = null;

    private bool _isGrounded = false;
    private bool _isJumping = false;

    private float _jumpTime = 0.5f;
    private float _jumpTimer = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.Instance.IsGameOver = false;
        _isGrounded = false;
        _isJumping = false;
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused)
            return;

        if (transform.position.y < -7)
        {
            rb.simulated = false;

            ScoreManager.SetHighScore();

            SceneManager.LoadScene("GameOver");
        }

        if (GameManager.Instance.IsGameOver)
            return;

        if (transform.position.y < 0.7)
        {
            GameOverAudioSource.Play();

            GameManager.Instance.IsGameOver = true;
            rb.freezeRotation = false;
            rb.angularVelocity = -100f;

            return;
        }

        bool wasGrounded = _isGrounded;

        _isGrounded = Physics2D.OverlapCircle(GroundChecker.position, 0.2f, GroundLayer) || rb.velocity == Vector2.zero;

        if (_isGrounded)
        {
            if (!wasGrounded && rb.velocity.y < 0)
                LandAudioSource.Play();

            //if (Input.GetKeyDown(KeyCode.Space))
            if (Input.GetButtonDown("Fire1"))
            {
                JumpAudioSource.Play();

                //rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rb.velocity = new Vector2(rb.velocity.x, 2);
                _isJumping = true;
                _jumpTimer = _jumpTime;
            }

            rb.velocity = new Vector2(4, rb.velocity.y);
        }

        if (_isJumping)
        {
            //if (Input.GetKey(KeyCode.Space))
            if (Input.GetButton("Fire1"))
            {
                if (_jumpTimer > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, (_jumpTimer > _jumpTime - 0.1 ? 2 : 4));
                    _jumpTimer -= Time.deltaTime;
                }
                else
                {
                    _isJumping = false;
                    _jumpTimer = 0;
                }
            }
        }

        //if (Input.GetKeyUp(KeyCode.Space))
        if (Input.GetButtonUp("Fire1"))
            _isJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Collectable>()?.Collect();
    }
}
