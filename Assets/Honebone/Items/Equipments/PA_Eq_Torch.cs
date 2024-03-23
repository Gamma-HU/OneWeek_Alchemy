using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Eq_Torch : PassiveAbility
{
    [SerializeField]
    GameObject blind;
    [SerializeField]
    BattleManager.Action action;
    bool f;
    public override void OnBattleStart()
    {
        f = false;
    }

    public override void OnAppliedStE(BattleManager.StEParams applied)
    {
        if (!f&&applied.StE == blind)
        {
            f = true;
            battleManager.Enqueue(character, character, action);
        }
    }
}
