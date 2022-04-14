using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveHUD : MonoBehaviour
{
    public Text FireMaxUsage;
    public Text FireCurrentUsage;

    public Text IceMaxUsage;
    public Text IceCurrentUsage;

    public Text WaterMaxUsage;
    public Text WaterCurrentUsage;

    public Text ElecMaxUsage;
    public Text ElecCurrentUsage;

    private void Awake() {
        FireMaxUsage.text = "" + Player.maxFireUsage;
        IceMaxUsage.text = "" + Player.maxIceUsage;
        WaterMaxUsage.text = "" + Player.maxWaterUsage;
        ElecMaxUsage.text = "" + Player.maxElecUsage;

        FireCurrentUsage.text = Player.fireUsage + "/";
        IceCurrentUsage.text = Player.iceUsage + "/";
        WaterCurrentUsage.text = Player.waterUsage + "/";
        ElecCurrentUsage.text = Player.elecUsage + "/";
    }
}
