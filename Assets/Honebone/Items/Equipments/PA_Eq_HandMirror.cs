using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Eq_HandMirror : PassiveAbility
{
    [SerializeField]
    GameObject strength;
    [SerializeField]
    GameObject guard;
    [SerializeField]
    BattleManager.Action action_str;
    [SerializeField]
    BattleManager.Action action_guard;


    public override void OnAttacked(int DMG, bool missed)
    {
        if (character.GetOpponent().CheckHasStE(guard))
        {
            battleManager.Enqueue(character, character, action_guard, GetPAIcon());
        }
    }
    public override void OnAttack(int DMG, bool missed)
    {
        if (character.GetOpponent().CheckHasStE(strength))
        {
            battleManager.Enqueue(character, character, action_str, GetPAIcon());
        }
    }
}
