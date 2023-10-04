using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    GameObject player1, player2;
    public Color[] playerColors;
    public Sprite[] blockSprites;
    public Sprite[] ballSprites;
    public Sprite[] characterSprites;
    ParticleSystem winEffect;
    int hoopScore = 5;
    StartCountDownController countDownController;
    public List<GameObject> blocks;
    [HideInInspector]
    public bool allowCharacterMovement = false;
    public bool allowMouseInput = false;

    void Awake()
    {
        Instance = this;
        countDownController = GetComponent<StartCountDownController>();
    }

    public void Start()
    {
        RoundTimer.Instance.TimerEnded += TimerEnded;
        countDownController.OnCountdownComplete += StartMatch;
        InitializeGame();
        SoundManager.instance.PlayAmbientCrowd();
        countDownController.StartCountdown();
    }
    void OnDestroy()
    {
        RoundTimer.Instance.TimerEnded -= TimerEnded;
        countDownController.OnCountdownComplete -= StartMatch;
    }
    public void StartMatch()
    {
        allowCharacterMovement = true;
        FindObjectOfType<BallController>().GetComponent<Rigidbody2D>().velocity = Vector2.down;

        Invoke("StartMusic", 1.0f);  // Calls the StartMusic method after 1 second
    }

    private void StartMusic()
    {
        SoundManager.instance.PlayMusic(1, true);
    }
    // Start is called before the first frame update
    void InitializeGame()
    {
        winEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        GameObject[] _blocks = GameObject.FindGameObjectsWithTag("Wall");
        blocks = new List<GameObject>();
        foreach (GameObject b in _blocks)
        {
            blocks.Add(b);
        }

        player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.GetComponent<SpriteRenderer>().sprite = characterSprites[0];
        player1.GetComponent<CharacterUserControl>().color = playerColors[0];
        player2 = GameObject.FindGameObjectWithTag("Player2"); ;
        player2.GetComponent<SpriteRenderer>().sprite = characterSprites[1];
        player2.GetComponent<CharacterUserControl>().color = playerColors[1];

    }

    public void Score(int playerNumber)
    {
        //number of blocks per score
        int _hoopScore = hoopScore;
        foreach (GameObject b in blocks)
        {
            if (playerNumber > 0 && b.GetComponent<SpriteRenderer>().color != playerColors[playerNumber - 1] && _hoopScore >= 0)
            {
                Debug.Log("Cecking color " + playerColors[playerNumber - 1]);
                ScoreController.instance.CheckBlockAndDrawColor();
                b.GetComponent<WallController>().color = playerColors[playerNumber - 1];
                b.GetComponent<SpriteRenderer>().sprite = GameController.Instance.blockSprites[playerNumber];
                b.GetComponent<WallController>().m_ControllingPlayer = playerNumber;
                _hoopScore--;
            }
        }

        if (CheckBlocksForCompleteWin(playerColors[playerNumber - 1]))
        {
            Win(playerNumber);
        }
    }
    public int ScoreGame()
    {
        int p1Score = 0, p2Score = 0;
        foreach (GameObject block in blocks)
        {
            WallController wall = block.GetComponent<WallController>();
            if (wall.m_ControllingPlayer == 0) continue;
            if (wall.m_ControllingPlayer == 1)
            {
                p1Score++;
            } else
            {
                p2Score++;
            }
        }
        if (p1Score == 0 && p2Score == 0 || p1Score == p2Score)
        {
            return 0;
        }
        else if (p1Score > p2Score)
        {
            return 1;
        }
        else return 2;
    }

    public void TimerEnded()
    {
        Win(ScoreGame());
    }
    public void Win(int playerNumber)
    {
        if(playerNumber != 0)
        {
            winEffect.startColor = playerColors[playerNumber - 1];
        }
        winEffect.Play();
        SoundManager.instance.PlayWinSound();
        CameraShake.Instance.shakeDuration = 10f;
        CameraShake.Instance.TriggerShake();
        allowCharacterMovement = false;
        countDownController.TriggerWin(playerNumber);
    }
    public bool CheckBlocksForCompleteWin(Color c)
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
