using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator Transition;

    public void LoadMenu() {
        StartCoroutine(LoadLevel(0));
    }

    public void LoadNarration() {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadBattle() {
        StartCoroutine(LoadLevel(4));
    }

    public void LoadBossBattle() {
        StartCoroutine(LoadLevel(5));
    }

    public void LoadOverworld() {
        if (StaticStats.isInside) {
            StartCoroutine(LoadLevel(3));
        }
        else {
            StartCoroutine(LoadLevel(2));
        }
    }

    private IEnumerator LoadLevel(int levelIndex) {
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }
}
