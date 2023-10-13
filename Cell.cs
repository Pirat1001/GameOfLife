using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive = false;
    public int neighbors = 0;

    SpriteRenderer spriteRenderer;

    public void SetAlive(bool alive)
    {
        isAlive = alive;

        if (alive)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}