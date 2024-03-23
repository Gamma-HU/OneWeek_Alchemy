using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_PoisonFrog : PassiveAbility
{
    [SerializeField]
    BattleManager.Action action;
    int count;
    public override void OnAttack(int DMG, bool missed)
    {
        count++; 
        if (count == 2)
        {
            count = 0;
            battleManager.Enqueue(character, character.GetOpponent(), action);
        }
    }
}
