using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit
{
    public void BecomeWeakFire() {
        this.FireResistance = -120;
        this.IceResistance = 100;
        this.WaterResistance = 100;
        this.ElectricResistance = 100;
    }

    public void BecomeWeakIce() {
        this.FireResistance = 100;
        this.IceResistance = -120;
        this.WaterResistance = 100;
        this.ElectricResistance = 100;
    }

    public void BecomeWeakWater() {
        this.FireResistance = 100;
        this.IceResistance = 100;
        this.WaterResistance = -120;
        this.ElectricResistance = 100;
    }

    public void BecomeWeakElec() {
        this.FireResistance = 100;
        this.IceResistance = 100;
        this.WaterResistance = 100;
        this.ElectricResistance = -120;
    }
}
