using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    public GameObject blockPrefab; // Assign your block prefab here
    public int numberOfBlocks = 34;
    public float radius = 1.5f;
    public List<SpriteRenderer> scoreBlocks;
    Color blankBlockColor;

    private void Start()
    {
        instance = this;
        ScoreController.instance = this;
        float angularDifference = 360f / numberOfBlocks;
        GameObject[] blocks = new GameObject[numberOfBlocks];
        for (int i = 0; i < numberOfBlocks; i++)
        {
            float offsetAngle = angularDifference / 2f;

            float x = radius * Mathf.Cos((i * angularDifference + offsetAngle) * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin((i * angularDifference + offsetAngle) * Mathf.Deg2Rad);


            GameObject block = Instantiate(blockPrefab, new Vector3(x, y, 0), Quaternion.Euler(0, 0, i * angularDifference));
            if (i == 0)
            {
                blankBlockColor = block.GetComponent<SpriteRenderer>().color;
            }
            block.transform.SetParent(transform); // Make the new block a child of the current object for organization
            blocks[i] = block;
        }
        scoreBlocks = new List<SpriteRenderer>();
        for (int i = 0; i < numberOfBlocks; i++)
        {
            if (i == 0) blankBlockColor = blocks[i].GetComponent<SpriteRenderer>().color;
            scoreBlocks.Add(blocks[i].GetComponent<SpriteRenderer>());
        }
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
                scoreBlocks[i].color = GameController.Instance.playerColors[0];
            } else if(numberOfBlocks - i <= p2)
            {
                scoreBlocks[i].color = GameController.Instance.playerColors[1];
            } else
            {
                scoreBlocks[i].color = blankBlockColor;

            }
        }
    }
}
