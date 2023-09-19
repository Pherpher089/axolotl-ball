using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public SpriteRenderer m_SpriteRenderer;
    public int m_ControllingPlayer = 0;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Ball" && other.collider.gameObject.GetComponent<BallController>().m_ControllingPlayer != 0)
        {
            //Color c = other.collider.gameObject.GetComponent<SpriteRenderer>().color;
            //m_SpriteRenderer.color = c;
        }
    }
}
