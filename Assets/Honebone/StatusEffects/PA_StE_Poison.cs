using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_StE_Poison : PA_StatusEffects
{
    public override void OnAttack(int DMG, bool missed)
    {
        BattleManager.Action action = new BattleManager.Action();
        action.DMG = stack;
        battleManager.Enqueue(character, character, action);
    }
}
