using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_SnowMan : PassiveAbility
{
    [SerializeField]
    BattleManager.Action action;
    public override void OnDamaged(int DMG, bool byOpponent)
    {
        if (characterStatus.GetHPPercent() < 0.5f)
        {
            battleManager.Enqueue(character, character, action, GetPAIcon());

        }
    }
}
