using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string UnitName;
    public int UnitLevel;

    public int PhysDamage;

    public int MaxHP;
    public int CurrentHP;

    public int Defence;

    public int FireResistance;
    public int IceResistance;
    public int WaterResistance;
    public int ElectricResistance;

    private void Awake()
    {
        CurrentHP = MaxHP;
    }

    public bool TakeDamage(int damage) {
        int takenDamage = damage - Defence;

        if (takenDamage < 0) {
            takenDamage = 0;
        }

        CurrentHP -= takenDamage;

        return CurrentHP <= 0;
    }

    public bool TakeFireDamage(int damage) {
        int takenDamage = damage - Defence - FireResistance;

        if (takenDamage < 0) {
            takenDamage = 0;
        }

        CurrentHP -= takenDamage;

        return CurrentHP <= 0;
    }

    public bool TakeIceDamage(int damage) {
        int takenDamage = damage - Defence - IceResistance;

        if (takenDamage < 0) {
            takenDamage = 0;
        }

        CurrentHP -= takenDamage;

        return CurrentHP <= 0;
    }

    public bool TakeWaterDamage(int damage) {
        int takenDamage = damage - Defence - WaterResistance;

        if (takenDamage < 0) {
            takenDamage = 0;
        }

        CurrentHP -= takenDamage;

        return CurrentHP <= 0;
    }

    public bool TakeElecDamage(int damage) {
        int takenDamage = damage - Defence - ElectricResistance;

        if (takenDamage < 0) {
            takenDamage = 0;
        }

        CurrentHP -= takenDamage;

        return CurrentHP <= 0;
    }
}
