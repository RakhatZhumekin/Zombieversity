using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject Player;

    public Animator Transition;

    public void LoadBattle() {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadOverworld() {
        StartCoroutine(LoadLevel(0));
    }

    private IEnumerator LoadLevel(int levelIndex) {
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }
}
