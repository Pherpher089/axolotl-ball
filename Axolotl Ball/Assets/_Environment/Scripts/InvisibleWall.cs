using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    public bool invisible = true;
    // Start is called before the first frame update
    void Start()
    {
        if (invisible) GetComponent<SpriteRenderer>().enabled = false;
    }
}
