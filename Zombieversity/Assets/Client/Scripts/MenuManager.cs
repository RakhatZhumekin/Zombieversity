using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public SceneLoader sceneLoader;

    public void OnPlayButton() {
        sceneLoader.LoadNarration();
    }

    public void OnExitButton() {
        Application.Quit();
    }
}
