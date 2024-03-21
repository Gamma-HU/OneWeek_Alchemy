using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Test : PassiveAbility
{
    [SerializeField]
    BattleManager.Action action;

    public override void OnAttack(int DMG, bool missed)
    {
        FindObjectOfType<BattleManager>().Enqueue(character, character, action);
    }
}
