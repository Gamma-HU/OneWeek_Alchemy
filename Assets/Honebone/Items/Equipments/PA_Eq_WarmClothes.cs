using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Eq_WarmClothes : PassiveAbility
{
    [SerializeField]
    GameObject freeze;
    [SerializeField]
    BattleManager.Action action;
    bool f;
    public override void OnBattleStart()
    {
        f = false;
    }
    public override void OnAppliedStE(BattleManager.StEParams applied)
    {
        if (!f && applied.StE == freeze)
        {
            f = true;
            battleManager.Enqueue(character, character, action, GetPAIcon());
        }
    }
}
