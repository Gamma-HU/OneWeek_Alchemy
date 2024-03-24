using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Eq_PepperPowder : PassiveAbility
{
    [SerializeField]
    GameObject freeze;
    [SerializeField]
    GameObject stun;

    [SerializeField]
    BattleManager.Action action;

    bool f;
    public override void OnBattleStart()
    {
        f = false;
    }
    public override void OnAppliedStE(BattleManager.StEParams applied)
    {
        if (!f && (applied.StE == freeze || applied.StE == stun))
        {
            f = true;
            battleManager.Enqueue(character, character, action);
        }
    }
}
