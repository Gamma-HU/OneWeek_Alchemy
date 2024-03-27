using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Skeleton : PassiveAbility
{
    [SerializeField]
    BattleManager.Action blind;
    [SerializeField]
    BattleManager.Action curse;
    int cooldown;
    public override void OnBattleStart()
    {
        battleManager.Enqueue(character, character.GetOpponent(), blind, GetPAIcon());
    }
    public override void OnAttacked(int DMG, bool missed)
    {
        if (cooldown > 0)
        {
            cooldown--;

        }
        else
        {
            Debug.Log("ok");
            cooldown = 3;
            battleManager.Enqueue(character, character.GetOpponent(), curse, GetPAIcon());
        }
    }
}
