using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DE_Cave : DungeonEffect
{
    [SerializeField]
    BattleManager.Action action;
    public override void OnBattleStart()
    {
        battleManager.Enqueue(battleManager.GetPlayer(), battleManager.GetPlayer(), action);
    }
}
