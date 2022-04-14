using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public const int maxFireUsage = 20;
    public static int fireUsage = maxFireUsage;

    public const int maxIceUsage = 20;
    public static int iceUsage = maxIceUsage;

    public const int maxWaterUsage = 15;
    public static int waterUsage = maxWaterUsage;

    public const int maxElecUsage = 20;
    public static int elecUsage = 0;

    public int FireDamage;
    public int IceDamage;
    public int WaterDamage;
    public int ElecDamage;
}
