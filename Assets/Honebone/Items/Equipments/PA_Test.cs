using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Test : PassiveAbility
{
    [SerializeField]
    BattleManager.Action action;
    [SerializeField]
    BattleManager.Action bleed;

    public override void OnBattleStart()
    {
        battleManager.Enqueue(character, character, bleed);
    }
    public override void OnAttack(int DMG, bool missed)
    {
        FindObjectOfType<BattleManager>().Enqueue(character, character, action);
    }
}
