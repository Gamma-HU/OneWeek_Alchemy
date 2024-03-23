using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Eq_DisenchantStone : PassiveAbility
{
    [SerializeField]
    GameObject StE;
    [SerializeField]
    BattleManager.Action action;
    int cooldown;

    public override void OnAttack(int DMG, bool missed)
    {
        if (cooldown>0)
        {
            cooldown--;
        }
    }

    public override void OnAppliedStE(BattleManager.StEParams applied)
    {
        if (cooldown==0 && applied.StE == StE)
        {
            cooldown = 5;
            battleManager.Enqueue(character, character, action);
        }
    }
}
