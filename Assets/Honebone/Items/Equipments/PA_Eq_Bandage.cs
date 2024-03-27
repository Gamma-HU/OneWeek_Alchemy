using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Eq_Bandage : PassiveAbility
{
    [SerializeField]
    GameObject bleed;
    [SerializeField]
    BattleManager.Action action;
    public override void OnBattleStart()
    {
        if (character.CheckHasStE(bleed))
        {
            battleManager.Enqueue(character, character, action, GetPAIcon());
        }
    }
}
