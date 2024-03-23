using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : PassiveAbility
{

    [SerializeField][Header("プレイヤーオブジェクト")] private GameObject player;
    private Character characterPlayer;
    private BattleManager.StEParams stEParams = new BattleManager.StEParams();

    private void Start()
    {
        stEParams.StE = gameObject;
        stEParams.amount = 1;

        characterPlayer = player.GetComponent<Character>();
        characterPlayer.ApplyStE(stEParams);
    }

    public override void OnAttack(int DMG, bool missed)
    {
        //base.OnAttack(DMG, missed);
        Debug.Log(DMG);
    }


}
