using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Pause;
    public GameObject WarningPanel;

    public Text WarningText;

    private GameObject portal;
    private GameObject boss;

    private void Awake() {
        Debug.Log(StaticStats.ZombieNames.Count);

        portal = GameObject.FindGameObjectWithTag("Portal");

        if (portal != null) {
            portal.SetActive(false);
        }
    
        boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss != null) {
            boss.SetActive(false);
        }

        if (StaticStats.ZombieNames.Count > 0) {
            foreach (string zombie in StaticStats.ZombieNames) {
               Destroy(GameObject.Find(zombie)); 
            }
        }

        if (StaticStats.PickedItems.Count > 0) {
            foreach (string item in StaticStats.PickedItems) {
               Destroy(GameObject.Find(item)); 
            }
        }

        if (StaticStats.ZombieNames.Count == 5) {
            if (!StaticStats.isInside) {
                StartCoroutine(Warning(false));
            }
        }

        if (StaticStats.ZombieNames.Count >= 5) {
            StaticStats.PlayerPosition = new Vector3(0f, -3f, 0f);
            portal.SetActive(true);
        }

        if (StaticStats.ZombieNames.Count >= 10) {
            boss.SetActive(true);
            StartCoroutine(Warning(true));
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            Pause.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnContinue() {
        Time.timeScale = 1;
        Pause.SetActive(false);
    }

    public void OnQuit() {
        Application.Quit();
    }

    private IEnumerator Warning(bool boss) {
        WarningPanel.SetActive(true);

        if (boss) {
            WarningText.text = "You can now fight the boss!";
        }
        else {
            WarningText.text = "You can now go inside the uni!";
        }

        yield return new WaitForSeconds(2f);

        WarningPanel.SetActive(false);
    }
}