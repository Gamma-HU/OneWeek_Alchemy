﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [System.Serializable]
    public class CharacterStatus
    {
        public bool player;
        public string charaName;

        [Header("最大HP")] public int maxHP;
        [Header("攻撃力")] public int ATK;

        [Header("装備品、敵キャラの特性、状態異常\n(PassiveAbilityがアタッチされているGameObjrct)")] public List<GameObject> passiveAbilities;


        [Header("\n\n=====<以下のステータスは戦闘中に変化>=====\n\n")]
        [Header("攻撃時のダメージ量に加算・乗算")]
        public int exDMG_int;
        public float exDMG_mul;

        [Header("敵からの被ダメージ量に加算・乗算")]
        public int PROT_int;
        public float PROT_mul;

        [Header("被回復量に乗算")] public float RHeal_mul;

        [Header("現在HP")] public int HP;

        public bool dead;
        public int blind;
        public int stun;

        public float GetHPPercent()
        {
            return HP * 1f / maxHP * 1f;
        }
    }
    [SerializeField]
    CharacterStatus status;

    List<PassiveAbility> passiveAbilities = new List<PassiveAbility>();

    Character opponent;
    CharacterStatus opponetnStatus;
    BattleManager battleManager;
    GaugeManager HPGauge;
    StEIconManager iconManager;
    BattleAnimationManager animManager;


    public void Init(BattleManager bm,GaugeManager gauge)
    {
        battleManager = bm;
        HPGauge = gauge;
        iconManager = HPGauge.GetComponentInChildren<StEIconManager>();
        status.HP = status.maxHP;
        HPGauge.UpdateHPGauge(status.GetHPPercent());
        animManager = FindObjectOfType<BattleAnimationManager>();
        foreach (GameObject passiveAbility in status.passiveAbilities)//stausにあるパッシブアビリティから、スクリプトだけを抽出(誘発処理の際の簡略化のため)
        {
            var p = Instantiate(passiveAbility, transform);
            p.GetComponent<PassiveAbility>().Init(this,battleManager);
            passiveAbilities.Add(p.GetComponent<PassiveAbility>());

            SpriteRenderer sr = p.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = false;
            }

        }
    }
    public void Equip(List<GameObject> equipments)//プレイヤーのみ使用　探索開始時に持ち込んだ装備品を装備
    {
        foreach(GameObject e in equipments)
        {
            var p = Instantiate(e, transform);
            p.GetComponent<PassiveAbility>().Init(this, battleManager);
            SpriteRenderer sr = p.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = false;
            }
            
            passiveAbilities.Add(p.GetComponent<PassiveAbility>());
        }
        HPGauge.UpdateHPGauge(status.GetHPPercent());
    }


    public void SetOpponent(Character chara) //戦闘相手の設定
    {
        opponent = chara;
        opponetnStatus = opponent.GetCharacterStatus();
    }
    public void Attack()
    {
        if (status.stun == 0)
        {
            if (status.blind == 0)
            {
                float fDMG = status.ATK;//基礎ダメージ
                float exDMG_mul = Mathf.Max(0f, 1f + status.exDMG_mul - opponetnStatus.PROT_mul);//ダメージ倍率補正 = 1 + [自身の与ダメージ率補正] - [相手の被ダメージ率補正] (負にはならない)
                int exDMG_int = status.exDMG_int - opponetnStatus.PROT_int;//ダメージ実数補正 = [自身の与ダメージ補正] - [相手の被ダメージ補正]
                fDMG = Mathf.Max(0f, (fDMG * exDMG_mul) + exDMG_int);//ダメージ = ([基礎ダメージ] * [ダメージ倍率補正]) + [ダメージ実数補正]
                int DMG = Mathf.RoundToInt(fDMG);
                opponent.Damage(DMG, true);//四捨五入して相手のDamage関数に渡す
                OnAttack(DMG, false);
                opponent.OnAttacked(DMG, false);

                //Sound Effect
                battleManager.PlaySE_Attack();
            }
            else
            {
                OnAttack(0, true);
                opponent.OnAttacked(0, true);
                animManager.ShowDamageIndicator(opponent.gameObject, 0);
                Debug.Log("Miss");
            }
            //if (status.player) { animManager.PlayAttackAnimation(status); }
            animManager.PlayAttackAnimation(status);
        }
        else
        {
            animManager.ShowStun(gameObject);
            animManager.PlayDamagedAnimation(status);
            Debug.Log("行動不能!");
            OnStun();
        }
           
    }
    public void Damage(int DMG,bool byOpponent)
    {
        status.HP-= DMG;
        animManager.ShowDamageIndicator(gameObject,-DMG);
        HPGauge.UpdateHPGauge(status.GetHPPercent());
        Debug.Log(string.Format("{0}は{1}ダメージ(残り{2})", status.charaName, DMG, status.HP));
        OnDamaged(DMG, byOpponent);
        //if (status.player) { animManager.PlayDamagedAnimation(status); }
        animManager.PlayDamagedAnimation(status);
        if (status.HP <= 0) { Die(); }

        //Sound Effect
        battleManager.PlaySE_Attacked();
    }
    public void Heal(int value)
    {
        float exHeal = Mathf.Max(0f, 1 + status.RHeal_mul);
        int heal = Mathf.RoundToInt(value * exHeal);
        status.HP = Mathf.Min(status.HP + heal, status.maxHP);
        HPGauge.UpdateHPGauge(status.GetHPPercent());
        //if (status.player) { animManager.PlayHealedAnimation(status); }
        animManager.PlayHealedAnimation(status);
        animManager.ShowDamageIndicator(gameObject, value);
        Debug.Log(string.Format("{0}は{1}回復", status.charaName, heal));
        OnHealed(heal);

        //Sound Effect
        battleManager.PlaySE_Heal();
    }
    public void ApplyStE(BattleManager.StEParams stEParams)
    {
        string StEName = stEParams.StE.GetComponent<PassiveAbility>().GetPAName();
        bool found = false;
        foreach (PassiveAbility passiveAbility in new List<PassiveAbility>(passiveAbilities))
        {
            if (passiveAbility.GetPAName() == StEName)
            {
                found = true;
                passiveAbility.GetComponent<PA_StatusEffects>().AddStack(stEParams.amount);
            }
        }
        if (!found)
        {
            var p = Instantiate(stEParams.StE, transform);
            p.GetComponent<PassiveAbility>().Init(this, battleManager);
            p.GetComponent<PA_StatusEffects>().StEInit(stEParams.amount, iconManager.SetStEIcon(p, stEParams.amount));

            SpriteRenderer sr = p.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = false;
            }

            passiveAbilities.Add(p.GetComponent<PassiveAbility>());
        }
        Debug.Log(string.Format("{0}に{1}{2}を付与", status.charaName, StEName, stEParams.amount));
        animManager.ShowAbilityApplyed(gameObject, stEParams);
        OnAppliedStE(stEParams);

        if (stEParams.StE.GetComponent<PA_StatusEffects>().GetIsBuff())
        {
            //Sound Effect
            battleManager.PlaySE_ApplyBuff();
        }
        else
        {
            //Sound Effect
            battleManager.PlaySE_ApplyDebuff();
        }
        
    }
    public void RemoveStE(GameObject remove)
    {
        string StEName = remove.GetComponent<PassiveAbility>().GetPAName();
        foreach (PassiveAbility passiveAbility in new List<PassiveAbility>(passiveAbilities))
        {
            if (passiveAbility.GetPAName() == StEName)
            {
                passiveAbility.GetComponent<PA_StatusEffects>().DisableStE();
                DisableStE(passiveAbility);
                Debug.Log(string.Format("{0}の{1}を除去", status.charaName, StEName));
                animManager.ShowAbilityRemoved(gameObject, remove);
                //animManager.ShowAbilityRemoved(gameObject, remove);

                
            }
        }
    }
    public void DisableStE(PassiveAbility remove)
    {
        passiveAbilities.Remove(remove);
    }
    void Die()
    {
        //===============================================[[死亡時演出]]===================================================
        Debug.Log(string.Format("{0}はたおれた", status.charaName));
        status.dead = true;
        animManager.PlayDisappearAnimation(status);
    }

    //-----------------------------------------------------<以下誘発処理>-----------------------------------------------------

    public void OnBattleStart()
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnBattleStart(); }
    }
    public void OnStun()
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnStun(); }
    }
    /// <summary>攻撃時、命中したかに関わらず誘発</summary>
    public void OnAttack(int DMG, bool missed)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnAttack(DMG, missed); }
    }
    /// <summary>攻撃された時、命中したかに関わらず誘発</summary>
    public void OnAttacked(int DMG, bool missed)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnAttacked(DMG, missed); }
    }

    /// <summary>被ダメージ時誘発</summary>
    public void OnDamaged(int DMG, bool byOpponent)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnDamaged(DMG, byOpponent); }
    }

    /// <summary>被回復時誘発</summary>
    public void OnHealed(int healedValue)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnHealed(healedValue); }
    }
    /// <summary>状態異常付与された時誘発</summary>
    public void OnAppliedStE(BattleManager.StEParams applied)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnAppliedStE(applied); }
    }

    public CharacterStatus GetCharacterStatus() { return status; }
    public Character GetOpponent() { return opponent; }
    public bool CheckHasStE(GameObject serch)
    {
        string StEName = serch.GetComponent<PassiveAbility>().GetPAName();
        foreach (PassiveAbility passiveAbility in new List<PassiveAbility>(passiveAbilities))
        {
            if (passiveAbility.GetPAName() == StEName)
            {
               return true;
            }
        }
        return false;
    }
    public string GetInfo()
    {
        string s = "";
        s += string.Format("体力：{0}/{1}", status.HP, status.maxHP);
        s += string.Format("攻撃力：{0}",status.ATK);
        //各PssiveAbilityから
        return s;
    }
}
