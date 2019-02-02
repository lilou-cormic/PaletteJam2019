using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    private Transform GroundChecker = null;

    [SerializeField]
    private LayerMask GroundLayer = 0;

    private bool _isGrounded = false;
    private bool _isJumping = false;

    private float _jumpTime = 0.35f;
    private float _jumpTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.Instance.IsGameOver = false;
        _isGrounded = false;
        _isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            rb.simulated = false;
        }

        if (GameManager.Instance.IsGameOver)
            return;

        if (transform.position.y < 0.7)
        {
            GameManager.Instance.IsGameOver = true;
            rb.freezeRotation = false;
            rb.angularVelocity = -100f;

            Debug.Log("Score: " + ScoreManager.Score + ", Best: " + ScoreManager.HighScore);

            return;
        }

        _isGrounded = Physics2D.OverlapCircle(GroundChecker.position, 0.2f, GroundLayer);

        if (_isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rb.velocity = new Vector2(rb.velocity.x, 3);
                _isJumping = true;
                _jumpTimer = _jumpTime;
            }

            rb.velocity = new Vector2(4, rb.velocity.y);
        }

        if (_isJumping)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (_jumpTimer > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 4);
                    _jumpTimer -= Time.deltaTime;
                }
                else
                {
                    _isJumping = false;
                    _jumpTimer = 0;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
            _isJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Collectable>()?.Collect();
    }
}
