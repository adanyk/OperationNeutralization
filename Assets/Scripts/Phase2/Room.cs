using System;
using System.Linq;
using UnityEngine;

public enum Walls
{
    frontWall = 0,
    leftWall = 1,
    backWall = 2,
    rightWall = 3,
    floor = 4,
    ceiling = 5
}

public class Room : MonoBehaviour
{
    public GameObject[] walls;
    public int[] hitsPerWall = new int[6];
    public GameObject gun;
    private readonly Color neutralizedColor = Color.green;
    private readonly Color tooManyShotsColor = Color.yellow;
    private readonly Color wrongGaugeSettingColor = Color.gray;
    private Color omittedColor;

    private void Awake()
    {
        omittedColor = GetComponentInChildren<MeshRenderer>().material.color;
    }

    public void HitWall(GameObject wall)
    {
        var wallIndex = (int)Enum.Parse(typeof(Walls), wall.tag);
        var numberOfHits = ++hitsPerWall[wallIndex];

        
        if (numberOfHits == 2 && wall.GetComponent<Renderer>().material.color == neutralizedColor)
        {
            ChangeWallColor(wall, tooManyShotsColor);
        }
        else if (numberOfHits == 1)
        {
            if (gun.GetComponent<GunRegulation>().gunPower != 0)
            {
                ChangeWallColor(wall, wrongGaugeSettingColor);
            }
            else
            {
                ChangeWallColor(wall, neutralizedColor);
            }
        }
    }

    private void ChangeWallColor(GameObject wall, Color color)
    {
        wall.GetComponent<Renderer>().material.color = color;
    }

    public bool IsRoomNeutralized()
    {
        return walls.All(wall => wall.GetComponent<Renderer>().material.color == neutralizedColor);
    }

    public int WallsShotWithWrongGaugeSetting()
    {
        return walls.Count(wall => wall.GetComponent<Renderer>().material.color == wrongGaugeSettingColor);
    }

    public int OmittedWalls()
    {
        return walls.Count(wall => wall.GetComponent<Renderer>().material.color == omittedColor);
    }

    public int WallsShotTooManyTimes()
    {
        return walls.Count(wall => wall.GetComponent<Renderer>().material.color == tooManyShotsColor);
    }
}
