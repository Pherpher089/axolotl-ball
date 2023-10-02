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
            SoundManager.instance.PlayBallHitWall();
            Color c = other.collider.gameObject.GetComponent<SpriteRenderer>().color;

            if(c == m_SpriteRenderer.color)
            {
                Debug.Log("### here 0 " + c.ToString() + " " + m_SpriteRenderer.color.ToString());

                return;
            }
            m_SpriteRenderer.color = c;
            m_ControllingPlayer = other.collider.gameObject.GetComponent<BallController>().m_ControllingPlayer;
            ScoreController.instance.CheckBlockAndDrawColor();
            if (GameController.Instance.CheckBlocksForCompleteWin(c))
            {
                GameController.Instance.Win(m_ControllingPlayer);
            }
        }
    }
}
