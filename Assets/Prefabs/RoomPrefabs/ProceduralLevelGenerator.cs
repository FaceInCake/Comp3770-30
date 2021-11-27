using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevelGenerator : MonoBehaviour
{
    public int numberOfRooms = 10;

    public GameObject startingRoomRef;
    public GameObject endingRoom;

    public GameObject[] rooms;

    const int gridWidth = 10;
    const int gridHeight = 10;
    GameObject[,] grid = new GameObject[gridWidth, gridHeight];

    void Start()
    {
        generateNewLevel(numberOfRooms);
    }

    void clearLevel()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                if (grid[i, j] != null)
                {
                    Destroy(grid[i, j]);
                    grid[i, j] = null;
                }
            }
        }
    }

    void generateNewLevel(int numRooms)
    {
        clearLevel();

        GameObject sRoom = (GameObject)Instantiate(startingRoomRef, new Vector3(0, 0, 0), Quaternion.identity);
        sRoom.GetComponent<RoomBrain>().init();
        grid[2, 2] = sRoom;

        int cx = 2, cy = 2;
        for (int i = 0; i < numRooms; i++)
        {
            int direction = Random.Range(0, 5);
            int dx = 0, dy = 0;
            if (direction == 0) { dx = 0; dy = 1; } // up
            if (direction == 1) { dx = 0; dy = -1; } // down
            if (direction == 2) { dx = -1; dy = 0; } // left
            if (direction == 3) { dx = 1; dy = 0; } // right

            // if new room is not in the grid, pick new random direction
            if (!(0 <= cx + dx && cx + dx < gridWidth))
            {
                i--;
                continue;
            }
            if (!(0 <= cy + dy && cy + dy < gridHeight))
            {
                i--;
                continue;
            }

            // if there is already a room there, open doors to it then pick new random direction
            if (grid[cx + dx, cy + dy] != null)
            {
                openDoors(cx, cy, dx, dy);
                cx += dx;
                cy += dy;

                i--;
                continue;
            } else
            {
                if (i == numRooms - 1)
                {
                    addEndRoom(cx + dx, cy + dy);
                } else
                {
                    addRandomRoom(cx + dx, cy + dy);
                }
                openDoors(cx, cy, dx, dy);
            }

        }

    }

    void openDoors(int cx, int cy, int dx, int dy)
    {
        if (dx == 1)
        {
            grid[cx, cy].GetComponent<RoomBrain>().openDoorPX();
            grid[cx + dx, cy].GetComponent<RoomBrain>().openDoorNX();
        }
        if (dx == -1)
        {
            grid[cx, cy].GetComponent<RoomBrain>().openDoorNX();
            grid[cx + dx, cy].GetComponent<RoomBrain>().openDoorPX();
        }
        if (dy == 1)
        {
            grid[cx, cy].GetComponent<RoomBrain>().openDoorPZ();
            grid[cx, cy + dy].GetComponent<RoomBrain>().openDoorNZ();
        }
        if (dy == -1)
        {
            grid[cx, cy].GetComponent<RoomBrain>().openDoorNZ();
            grid[cx, cy + dy].GetComponent<RoomBrain>().openDoorPZ();
        }
    }

    void addRandomRoom(int x, int y)
    {
        int rrIndex = Random.Range(0, rooms.Length);
        GameObject randRoom = (GameObject)Instantiate(rooms[rrIndex], new Vector3((x - 2) * 20, 0, (y - 2) * 20), Quaternion.identity);

        grid[x, y] = randRoom;
        grid[x, y].GetComponent<RoomBrain>().init();
    }

    void addEndRoom(int x, int y)
    {
        grid[x, y] = (GameObject)Instantiate(endingRoom, new Vector3((x - 2) * 20, 0, (y - 2) * 20), Quaternion.identity); ;
        grid[x, y].GetComponent<RoomBrain>().init();
    }

}
