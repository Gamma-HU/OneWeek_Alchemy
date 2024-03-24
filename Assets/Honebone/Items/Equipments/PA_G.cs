using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_G : PassiveAbility
{
    [SerializeField,Header("戦闘開始時　相手")] bool BattleStart_oppo;
    [SerializeField] BattleManager.Action action_BS_Oppo;
    [SerializeField, Header("\n\n戦闘開始時　自分")] bool BattleStart_self;
    [SerializeField] BattleManager.Action action_BS_self;

    [SerializeField, Header("\n\n攻撃時　相手")] bool attack_oppo;
    [SerializeField] bool onlyHit_attack_oppo;
    [SerializeField] int cd_attack_oppo;
    [SerializeField] BattleManager.Action action_attack_oppo;
    
    [SerializeField, Header("\n\n攻撃時　自分")] bool attack_self;
    [SerializeField] bool onlyHit_attack_self;
    [SerializeField] int cd_attack_self;
    [SerializeField] BattleManager.Action action_attack_self;

    [SerializeField, Header("\n\n被攻撃時　相手")] bool attacked_oppo;
    [SerializeField] bool onlyHit_attacked_oppo;
    [SerializeField] int cd_attacked_oppo;
    [SerializeField] BattleManager.Action action_attacked_oppo;

    [SerializeField, Header("\n\n被付与時　自分")] bool appied_self;
    [SerializeField] int cd_applied_self;
    [SerializeField] GameObject appiedCheck_self;
    [SerializeField] BattleManager.Action action_applied_self;

    int count_attack;
    int count_attack_self;

    int count_attacked_oppo;

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
        count_attack = 0;
        count_attack_self = 0;
        count_applied = 0;
    }
    public override void OnAttack(int DMG, bool missed)
    {
        if (attack_oppo && !(onlyHit_attack_oppo && missed))
        {
            count_attack++;
            if (count_attack >= cd_attack_oppo)
            {
                count_attack = 0;
                battleManager.Enqueue(character, character.GetOpponent(), action_attack_oppo);
            }
        }
        
        if (attack_self && !(onlyHit_attack_self && missed))
        {
            count_attack_self++;
            if (count_attack_self >= cd_attack_self)
            {
                count_attack_self = 0;
                battleManager.Enqueue(character, character, action_attack_self);
            }
        }

    }
    public override void OnAttacked(int DMG, bool missed)
    {
        if (attacked_oppo && !(onlyHit_attacked_oppo && missed))
        {
            count_attacked_oppo++;
            if (count_attacked_oppo >= cd_attack_oppo)
            {
                count_attacked_oppo = 0;
                battleManager.Enqueue(character, character.GetOpponent(), action_attacked_oppo);
            }
        }
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
