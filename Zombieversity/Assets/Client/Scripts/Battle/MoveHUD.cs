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
        FireMaxUsage.text = "" + Player.MaxFireUsage;
        IceMaxUsage.text = "" + Player.MaxIceUsage;
        WaterMaxUsage.text = "" + Player.MaxWaterUsage;
        ElecMaxUsage.text = "" + Player.MaxElecUsage;

        FireCurrentUsage.text = Player.FireUsage + "/";
        IceCurrentUsage.text = Player.IceUsage + "/";
        WaterCurrentUsage.text = Player.WaterUsage + "/";
        ElecCurrentUsage.text = Player.ElecUsage + "/";
    }
}
