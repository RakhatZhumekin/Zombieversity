using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject[] Zombies = new GameObject[4];

    public int numOfZombies;

    public int randomIndex;
    public GameObject currentZombieType;

    private void Awake() {
        Debug.Log("BATTLE MANAGER");

        numOfZombies = Mathf.FloorToInt(Random.Range(1f, 4f));

        randomIndex = Mathf.FloorToInt(Random.Range(0f, 4f));

        currentZombieType = Zombies[randomIndex];
    }
}
