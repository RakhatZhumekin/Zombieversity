using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public const int MaxFireUsage = 20;
    public static int FireUsage = MaxFireUsage;

    public const int MaxIceUsage = 20;
    public static int IceUsage = MaxIceUsage;

    public const int MaxWaterUsage = 15;
    public static int WaterUsage = MaxWaterUsage;

    public const int MaxElecUsage = 20;
    public static int ElecUsage = MaxElecUsage;

    public int FireDamage;
    public int IceDamage;
    public int WaterDamage;
    public int ElecDamage;
}
