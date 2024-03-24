using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEffect : MonoBehaviour
{
    [SerializeField, TextArea(3, 10)]
    string DEInfo;
    protected BattleManager battleManager;

    public void Init(BattleManager bm)
    {

        battleManager = bm;

    }
    public virtual void OnBattleStart() { }

    public string GetDEInfo() { return DEInfo; }
}
