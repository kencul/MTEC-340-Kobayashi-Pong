using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class ballMovement : MonoBehaviour
{
    private Vector2 velocity;
    private Rigidbody2D rb2D;

    public float yBound = 4.85f;
    public float xBound = 9.35f;

    public AudioClip hitSound;
    public AudioClip paddleHitSound;
    public AudioClip scoreSound;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetPos();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.State == "play")
        {
            rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);

            if (Mathf.Abs(rb2D.position.y) >= yBound)
            {
                GameManager.Instance.playSound(hitSound, 0.5f);
                WallCollision();
            }

            if (Mathf.Abs(rb2D.position.x) >= xBound)
            {
                Death();
                GameManager.Instance.playSound(scoreSound, 0.5f);
            }
        }
    }

    public void ResetPos()
    {
        if (GameManager.Instance.State == "play")
            GameManager.Instance.State = "serve";
        GameManager.Instance.messagesGUI.text = "Enter To Start";
        GameManager.Instance.messagesGUI.enabled = true;
        transform.position = new Vector3(0, 0, 0);
        velocity = new Vector2
        (
            GameManager.Instance.initBallSpeed * (Random.Range(0.0f, 1.0f)>0.5f ? 1f : -1f),
            GameManager.Instance.initBallSpeed * (Random.Range(0.0f, 1.0f) >0.5f ? 1f : -1f)
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("paddle"))
        {
            velocity.x *= -1;
            velocity.x = IncrementSpeed(velocity.x);
            GameManager.Instance.playSound(paddleHitSound, 0.5f);
        }
    }

    private float IncrementSpeed(float axis)
    {
        axis += axis > 0 ? GameManager.Instance.ballSpeedIncrement : -GameManager.Instance.ballSpeedIncrement;
        return axis;
    }

    private void WallCollision()
    {
        velocity.y *= -1;
        rb2D.MovePosition(new Vector2(rb2D.position.x, rb2D.position.y > 0 ? yBound - 0.01f : -yBound + 0.01f));
    }

    private void Death()
    {
        GameManager.Instance.updateScore((rb2D.position.x > 0) ? 1 : 2);
        ResetPos();
    }
}
