using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_G : PassiveAbility
{
    [SerializeField,Header("�퓬�J�n���@����")] bool BattleStart_oppo;
    [SerializeField] BattleManager.Action action_BS_Oppo;
    [SerializeField, Header("\n\n�퓬�J�n���@����")] bool BattleStart_self;
    [SerializeField] BattleManager.Action action_BS_self;

    [SerializeField, Header("\n\n��t�^���@����")] bool appied_self;
    [SerializeField] int cd_applied_self;
    [SerializeField] GameObject appiedCheck_self;
    [SerializeField] BattleManager.Action action_applied_self;

    int count_applied;
    public override void OnBattleStart()
    {
        if (BattleStart_oppo)
        {
            battleManager.Enqueue(character, character.GetOpponent(), action_BS_Oppo);
        }
        if (BattleStart_self)
        {
            battleManager.Enqueue(character, character, action_BS_self);
        }
        count_applied = 0;
    }
    public override void OnAppliedStE(BattleManager.StEParams applied)
    {
       
        count_applied++;
        if (appied_self)
        {
            if (count_applied >= cd_applied_self && applied.StE == appiedCheck_self)
            {
                count_applied = 0;
                battleManager.Enqueue(character, character, action_applied_self);
            }
        }
    }
}
