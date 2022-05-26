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

    public static List<string> PickedItems = new List<string>();

    public static bool isInside = false;

    public static int order = 0;

    public static bool isPrologue = true;
}
