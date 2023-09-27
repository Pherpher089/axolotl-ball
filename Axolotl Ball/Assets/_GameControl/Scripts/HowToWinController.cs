using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToWinController : MonoBehaviour
{
    void Update()
    {
        if ((Input.touchCount > 0  && Input.GetTouch(0).phase == TouchPhase.Began) || Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
