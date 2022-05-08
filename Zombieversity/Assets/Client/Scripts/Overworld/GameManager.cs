using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake() {
        if (StaticStats.ZombieNames.Count > 0) {
            foreach (string zombie in StaticStats.ZombieNames) {
               Destroy(GameObject.Find(zombie)); 
            }
        }
    }
}
