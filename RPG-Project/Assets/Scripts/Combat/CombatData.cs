using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatData
{
    private static Sprite combatbackGround = null;
    private static Enemy Combatenemy = null;

    public static void loadCombatData(Sprite CombatbackGround, Enemy combatEnemy)
    {
        combatbackGround = CombatbackGround;
        Combatenemy = combatEnemy;

    }

    public static void unloadCombatData()
    {
        combatbackGround = null;
        Combatenemy = null;

    }

    public static Sprite GetCombatBackGround() { return combatbackGround;}
    public static Enemy GetEnemy() { return Combatenemy; }




    //will serve as a container for the current combat that is happening and store values during the scene load, with the usual load functions for enemies, 
}
