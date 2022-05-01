using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticStats
{
    public static Vector3 PlayerPosition = new Vector3(0f, -3f, 0f);

    public static Vector3[] EnemyPositions =
         {new Vector3(-4f, 0.4f, 0f), new Vector3(-5.5f, 1f, 0f), 
            new Vector3(-5.5f, -1f, 0f)};

    public static List<string> ZombieNames = new List<string>();

    // public static Vector3 CampusPlayerSpawn = new Vector3(-5.6f, -1.6f, 0f);

    public static bool isInside = false;
}
