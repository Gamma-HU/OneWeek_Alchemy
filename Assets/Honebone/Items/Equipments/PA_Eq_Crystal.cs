using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Eq_Crystal : PassiveAbility
{
    [SerializeField]
    GameObject blind;
    [SerializeField]
    BattleManager.Action action;
    bool f;
    


    public override void OnDamaged(int DMG, bool byOpponent)
    {
        if (!f&&characterStatus.GetHPPercent() <=0.2f)
        {
            f = true;
            battleManager.Enqueue(character, character, action, GetPAIcon());

        }
    }
}
