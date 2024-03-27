using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Eq_HealPotionS : PassiveAbility
{
    [SerializeField]
    BattleManager.Action action;
    bool f;

    public override void OnDamaged(int DMG, bool byOpponent)
    {
        if (!f&&characterStatus.GetHPPercent() <= 0.5f)
        {
            f = true;
            battleManager.Enqueue(character, character, action, GetPAIcon());
        }
    }
}
