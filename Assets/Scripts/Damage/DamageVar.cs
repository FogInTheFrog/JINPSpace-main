using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVar : MonoBehaviour
{
    public static int AD = 0;
    public static float AS = 2f;
    public static int MS = 0;
    public static int HP = 0;

    void Start()
    {
        AD = 0;
        AS = 2f;
        MS = 0;
        HP = 0;
    }

    // when we hit bonus
    public static void LevelUpAD()
    {
        Debug.Log("AD Level up");
        AD += 1;
    }
    public static void LevelUpAS()
    {
        Debug.Log("AS Level up");
        AS += 1f;
        Weapon.SetNewRateOfFire(AS);
    }
    public static void LevelUpMS()
    {
        Debug.Log("MS Level up");
        MS += 1;
        PlayerController.SetNewMS();
    }
    public static void LevelUpHP()
    {
        Debug.Log("HP Level up");
        HP += 1;
        GameObject.Find("Player").GetComponent<DestructibleObject>().IncrementHP(HP);
    }

    public static void Reset()
    {
        AD = 0;
        AS = 2f;
        MS = 0;
        HP = 0;
    }
}
