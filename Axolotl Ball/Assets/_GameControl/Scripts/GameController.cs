using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    GameObject player1, player2;
    public Color[] playerColors;
    ParticleSystem winEffect;
    int hoopScore = 5;
    StartCountDownController countDownController;
    //[HideInInspector]
    public List<GameObject> blocks;
    Transform ballTransform;
    public bool allowCharacterMovement = false;
    void Awake()
    {
        Instance = this;
        countDownController = GetComponent<StartCountDownController>();
    }

    public void Start()
    {
        countDownController.OnCountdownComplete += StartMatch;
        InitializeGame();
        countDownController.StartCountdown();
    }
    void OnDestroy()
    {
        countDownController.OnCountdownComplete -= StartMatch;
    }
    public void StartMatch()
    {
        allowCharacterMovement = true;
        SoundManager.instance.PlayMusic(1, true);
        FindObjectOfType<BallController>().GetComponent<Rigidbody2D>().velocity = Vector2.down;
    }
    // Start is called before the first frame update
    void InitializeGame()
    {
        ballTransform = GameObject.FindGameObjectWithTag("Ball").transform;
        winEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        GameObject[] _blocks = GameObject.FindGameObjectsWithTag("Wall");
        blocks = new List<GameObject>();
        foreach (GameObject b in _blocks)
        {
            blocks.Add(b);
        }

        player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.GetComponent<SpriteRenderer>().color = playerColors[0];
        player2 = GameObject.FindGameObjectWithTag("Player2"); ;
        player2.GetComponent<SpriteRenderer>().color = playerColors[1];

    }

    public void Score(int playerNumber)
    {
        //number of blocks per score
        int _hoopScore = hoopScore;
        foreach (GameObject b in blocks)
        {
            if (playerNumber > 0 && b.GetComponent<SpriteRenderer>().color != playerColors[playerNumber - 1] && _hoopScore > 0)
            {
                Debug.Log("Cecking color " + playerColors[playerNumber - 1]);
                b.GetComponent<SpriteRenderer>().color = playerColors[playerNumber - 1];
                _hoopScore--;
            }
        }

        if (CheckScore(playerColors[playerNumber - 1]))
        {
            winEffect.startColor = playerColors[playerNumber - 1];
            winEffect.Play();
            allowCharacterMovement = false;
            countDownController.TriggerWin();
        }
    }
    bool CheckScore(Color c)
    {
        foreach (GameObject b in blocks)
        {
            if (b.GetComponent<SpriteRenderer>().color != c)
            {
                return false;
            }
        }
        return true;
    }
}
