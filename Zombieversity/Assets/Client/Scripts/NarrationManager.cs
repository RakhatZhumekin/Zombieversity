using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrationManager : MonoBehaviour
{
    public Text NarrationText;
    public Text ButtonText;
    public SceneLoader sceneLoader;

    public string[] ScriptPrologue = new string[6];
    public string[] ScriptEnding = new string[5];
    private int page = 0;

    private void Awake() {
        if (StaticStats.isPrologue) {
            NarrationText.text = ScriptPrologue[0];
        }
        else {
            NarrationText.text = ScriptEnding[0];
        }
    }

    private void Update() {
        if (StaticStats.isPrologue) {
            if (page == ScriptPrologue.Length - 1) {
                ButtonText.text = "Begin";
            }
        }
        else {
            if (page == ScriptEnding.Length - 1) {
                ButtonText.text = "Quit";
            }
        }
    }

    public void OnButton() {
        if (StaticStats.isPrologue) {
            if (page < ScriptPrologue.Length - 1) {
                ++page;
                NarrationText.text = ScriptPrologue[page];
            }
            else {
                sceneLoader.LoadOverworld();
                page = 0;
            }
        }
        else {
            if (page < ScriptEnding.Length - 1) {
                ++page;
                NarrationText.text = ScriptEnding[page];
            }
            else {
                Application.Quit();
            }
        }
    }
}
