using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopControl : MonoBehaviour
{
    public ParticleSystem m_ScoreEffect;
    void Start()
    {
        m_ScoreEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            GameController.Instance.Score(other.GetComponent<BallController>().m_ControllingPlayer);
            m_ScoreEffect.startColor = GameController.Instance.playerColors[other.GetComponent<BallController>().m_ControllingPlayer - 1];
            other.GetComponent<BallController>().SetBallNatural();
            m_ScoreEffect.Play();
            CameraShake.Instance.TriggerShake();
        }
    }
}
