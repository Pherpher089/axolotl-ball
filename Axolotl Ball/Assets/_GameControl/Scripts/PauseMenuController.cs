using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    GameObject uiParent;
    GameObject pauseMenu;
    GameObject quitConfirmationModal;
    public static PauseMenuController Instance;
    Vector3 ballDir = Vector3.zero;
    Scene currentScene;
    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        currentScene = SceneManager.GetActiveScene();
        uiParent = transform.GetChild(0).gameObject;
        quitConfirmationModal = uiParent.transform.GetChild(1).gameObject;
        pauseMenu = uiParent.transform.GetChild(0).gameObject;
        if(uiParent.activeSelf) uiParent.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!uiParent.activeSelf);
        }
    }
    public void PauseGame(bool _isPaused)
    {
        uiParent.SetActive(_isPaused);
        isPaused = _isPaused;
        if(_isPaused)
        {
            ballDir = GameObject.FindObjectOfType<BallController>().GetComponent<Rigidbody2D>().velocity;
            Rigidbody2D[] allRigidbodies = GameObject.FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.InstanceID);
            foreach(Rigidbody2D body in allRigidbodies) { 
                body.bodyType = RigidbodyType2D.Kinematic;
                body.velocity = Vector2.zero;
            };

        }
        else
        {
            Rigidbody2D[] allRigidbodies = GameObject.FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.InstanceID);
            foreach (Rigidbody2D body in allRigidbodies)
            {
                body.bodyType = RigidbodyType2D.Dynamic;
            };
            GameObject.FindObjectOfType<BallController>().GetComponent<Rigidbody2D>().velocity = ballDir;
        }
    }

    public void RestartScene()
    {
        if (Application.isPlaying)
        {
            isPaused = false;
            SceneManager.LoadScene("GameScene");
        }
    }

    public void QuitGame()
    {
        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }

    public void QuitToMainMenue()
    {
        if (Application.isPlaying)
        {
            //This assumes the main menu is the previous scene in the build index
            SceneManager.LoadScene(0);
        }
    }
    
    public void Cancel()
    {
        quitConfirmationModal.SetActive(false);
        pauseMenu.SetActive(true);

    }
    public void OnOpenConfirmationModal()
    {
        quitConfirmationModal.SetActive(true);
        pauseMenu.SetActive(false);

    }

    public void Resume()
    {
        PauseGame(false);
    }
}
