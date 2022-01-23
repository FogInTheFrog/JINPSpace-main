﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponList
{
    [SerializeField]
    private List<Weapon> m_Weapons = null;

    public void FireAll()
    {
        foreach (Weapon weapon in m_Weapons)
        {
            weapon.Fire();
        }
    }
}

