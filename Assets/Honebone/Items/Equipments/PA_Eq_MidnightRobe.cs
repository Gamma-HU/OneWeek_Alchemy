using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Eq_MidnightRobe : PassiveAbility
{
    [SerializeField]
    BattleManager.Action action;
    public override void OnBattleStart()
    {
        battleManager.Enqueue(character, character.GetOpponent(), action);
    }
}
