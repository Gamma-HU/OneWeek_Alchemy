using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_GoblinLeader : PassiveAbility
{
    [SerializeField]
    BattleManager.Action action;
    [SerializeField]
    BattleManager.Action weak;
    int count;

    public override void OnAttack(int DMG, bool missed)
    {
        count++;
        if (!missed)
        {
            battleManager.Enqueue(character, character.GetOpponent(), action, GetPAIcon());
        }
        if (count==3)
        {
            count = 0;
            battleManager.Enqueue(character, character.GetOpponent(), weak, GetPAIcon());
        }
    }
}
