using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Gblin : PassiveAbility
{
    [SerializeField]
    BattleManager.Action action;

    public override void OnAttack(int DMG, bool missed)
    {
        if (!missed)
        {
            battleManager.Enqueue(character, character.GetOpponent(), action,GetPAIcon());
        }
    }
}
