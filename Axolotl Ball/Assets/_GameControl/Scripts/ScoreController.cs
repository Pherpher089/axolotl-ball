using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    public GameObject[] pinkBlocks;
    public GameObject[] blueBlocks;
    public GameObject[] nutralBlocks;
    int numberOfBlocks = 34;

    Color blankBlockColor;

    private void Start()
    {
        instance = this;

    }

    public void CheckBlockAndDrawColor()
    {
        int p1 = 0, p2 = 0;
        foreach (GameObject block in GameController.Instance.blocks) { 
            SpriteRenderer blockRenderer = block.GetComponent<SpriteRenderer>();
            if (blockRenderer.GetComponent<WallController>().color == GameController.Instance.playerColors[0])
            {
                p1++;
            } else if (blockRenderer.GetComponent<WallController>().color == GameController.Instance.playerColors[1])
            {
                p2++;
            }
        }
        for(int i = 0; i< numberOfBlocks; i++)
        {
            if(i < p1)
            {
                pinkBlocks[i].SetActive(true);
                blueBlocks[i].SetActive(false);
                nutralBlocks[i].SetActive(false);
            } else if(numberOfBlocks - i <= p2)
            {
                pinkBlocks[i].SetActive(false);
                blueBlocks[i].SetActive(true);
                nutralBlocks[i].SetActive(false);
            } else
            {
                pinkBlocks[i].SetActive(false);
                blueBlocks[i].SetActive(false);
                nutralBlocks[i].SetActive(true);
            }
        }
    }
}
