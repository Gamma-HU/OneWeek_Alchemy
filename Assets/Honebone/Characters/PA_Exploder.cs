using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Exploder : PassiveAbility
{
    [SerializeField]
    BattleManager.Action action;
    bool f;

    public override void OnAttack(int DMG, bool missed)
    {
        if (!f && characterStatus.GetHPPercent() <= 0.25f)
        {
            f = true;
            battleManager.Enqueue(character, character, action,GetPAIcon());
        }
    }
}
