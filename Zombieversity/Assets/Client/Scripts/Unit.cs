using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string UnitName;
    public int UnitLevel;

    public int Damage;

    public int MaxHP;
    public int CurrentHP;

    public bool TakeDamage(int damage) {
        CurrentHP -= damage;

        return CurrentHP <= 0;
    }
}
