using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    private static int SCREEN_WIDTH = 64;   // 1024 - pixels
    private static int SCREEN_HEIGHT = 48;  // 768 - pixels

    public float speed = 0.1f;
    private float timer = 0;

    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];

    void Start()
    {


        PlaceCells();
    }

    void Update()
    {
        float simSpeed = PlayerPrefs.GetFloat("SimulationSpeed");

        if(timer >= simSpeed)
        {
            timer = 0;

            CountNeighbors();

            PopulationControl();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void PlaceCells()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                grid[x, y].SetAlive(RandomAliveCell());
            }
        }
    }

    void CountNeighbors()
    {
        for(int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for(int x = 0; x < SCREEN_WIDTH; x++)
            {
                int numNeighbors = 0;

                // North
                if(y+1 < SCREEN_HEIGHT)
                {
                    if (grid[x, y+1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                // East
                if(x+1 < SCREEN_WIDTH)
                {
                    if (grid[x+1, y].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                // South 
                if(y-1 >= 0)
                {
                    if (grid[x, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                // West
                if(x-1 >= 0)
                {
                    if (grid[x-1, y].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                // North-East
                if(x+1 < SCREEN_WIDTH && y+1 < SCREEN_HEIGHT)
                {
                    if (grid[x+1, y+1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                // North-West
                if(x-1 >= 0 && y+1 < SCREEN_HEIGHT) 
                {
                    if (grid[x-1, y+1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                // South-East 
                if(x+1 < SCREEN_WIDTH && y-1 >= 0)
                {
                    if (grid[x+1, y-1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                // South-West 
                if(x-1 >= 0 && y-1 >= 0)
                {
                    if (grid[x-1, y-1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                grid[x, y].neighbors = numNeighbors;
            }
        }
    }

    void PopulationControl()
    {
        for(int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for(int x = 0; x < SCREEN_WIDTH; x++)
            {
                if (grid[x, y].isAlive)
                {
                    // Cell is alive
                    if (grid[x, y].neighbors != 2 && grid[x, y].neighbors != 3)
                    {
                        grid[x, y].SetAlive(false);
                    }
                }
                else
                {
                    // Cell is dead
                    if (grid[x, y].neighbors == 3)
                    {
                        grid[x, y].SetAlive(true);
                    }
                }
            }
        }
    }

    bool RandomAliveCell()
    {
        float s_percentage = PlayerPrefs.GetFloat("SpawnPercentage");

        int rand = UnityEngine.Random.Range(0, 100);

        if(rand <= s_percentage)
        {
            return true;
        }

        return false;
    }
}