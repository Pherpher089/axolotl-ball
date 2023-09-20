using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    public int m_ControllingPlayer;
    public Color m_neutralBallColor;
    TrailRenderer m_TrailRenderer;
    public float maxSpeed = 5f; // Adjust the speed as needed
    private Rigidbody2D rb;
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_TrailRenderer = GetComponent<TrailRenderer>();
        m_SpriteRenderer.color = m_neutralBallColor;
        m_TrailRenderer.material.color = m_neutralBallColor;
        rb = GetComponent<Rigidbody2D>();

        // Set initial velocity
    }
    private void FixedUpdate()
    {
        // Ensure the velocity's magnitude does not exceed the maximum speed
        if (rb.velocity.magnitude > maxSpeed || rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Player1" || other.collider.gameObject.tag == "Player2")
        {
            SoundManager.instance.PlayPlayerHitBall();
            m_ControllingPlayer = other.collider.gameObject.GetComponent<CharacterUserControl>().m_PlayerNumber;
            Color c = other.collider.gameObject.GetComponent<SpriteRenderer>().color;
            m_SpriteRenderer.color = c;
            m_TrailRenderer.material.color = c;
        }
    }

    public void SetBallNatural()
    {
        m_SpriteRenderer.color = m_neutralBallColor;
        m_TrailRenderer.material.color = m_neutralBallColor;
        m_ControllingPlayer = 0;
    }
}
