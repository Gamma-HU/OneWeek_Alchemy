using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_Zombie : PassiveAbility
{
    [SerializeField]
    BattleManager.Action heal;
    [SerializeField]
    BattleManager.Action poison;
    int count;
    public override void OnAttack(int DMG, bool missed)
    {
        if (!missed)
        {
            count++;
            if (count == 2)
            {
                count = 0;
                battleManager.Enqueue(character, character.GetOpponent(), poison);
                battleManager.Enqueue(character, character, heal);
            }    
        }
    }
}
