using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [System.Serializable]
    public class CharacterStatus
    {
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

        public int blind;
    }
    [SerializeField]
    CharacterStatus status;

    List<PassiveAbility> passiveAbilities = new List<PassiveAbility>();

    Character opponent;
    CharacterStatus opponetnStatus;

    public void Init()
    {
        status.HP = status.maxHP;
        foreach (GameObject passiveAbility in status.passiveAbilities)//stausにあるパッシブアビリティから、スクリプトだけを抽出(誘発処理の際の簡略化のため)
        {
            var p = Instantiate(passiveAbility, transform);
            p.GetComponent<PassiveAbility>().Init(this);
            passiveAbilities.Add(p.GetComponent<PassiveAbility>());
        }
    }


    public void SetOpponent(Character chara) //戦闘相手の設定
    {
        opponent = chara;
        opponetnStatus = opponent.GetCharacterStatus();
    }
    public void Attack()
    {
        if (status.blind == 0)
        {
            float fDMG = status.ATK;//基礎ダメージ
            float exDMG_mul = Mathf.Max(0f, 1f + status.exDMG_mul - opponetnStatus.PROT_mul);//ダメージ倍率補正 = 1 + [自身の与ダメージ率補正] - [相手の被ダメージ率補正] (負にはならない)
            int exDMG_int = status.exDMG_int - opponetnStatus.PROT_int;//ダメージ実数補正 = [自身の与ダメージ補正] - [相手の被ダメージ補正]
            fDMG = Mathf.Max(0f, (fDMG * exDMG_mul) + exDMG_int);//ダメージ = ([基礎ダメージ] * [ダメージ倍率補正]) + [ダメージ実数補正]
            int DMG = Mathf.RoundToInt(fDMG);
            opponent.Damage(DMG,true);//四捨五入して相手のDamage関数に渡す
            OnAttack(DMG, false);
            opponent.OnAttacked(DMG, false);
        }
        else
        {
            OnAttack(0, true);
            opponent.OnAttacked(0, true);
        }
    }
    public void Damage(int DMG,bool byOpponent)
    {
        Debug.Log(string.Format("{0}は{1}ダメージ", status.charaName, DMG));
        status.HP-= DMG;
        OnDamaged(DMG, byOpponent);
        if (status.HP <= 0) { Die(); }
    }
    public void Heal(int value)
    {
        float exHeal = Mathf.Max(0f, 1 + status.RHeal_mul);
        int heal = Mathf.RoundToInt(value * exHeal);
        status.HP += Mathf.Min(status.HP + heal, status.maxHP);
        Debug.Log(string.Format("{0}は{1}回復", status.charaName, heal));
        OnHealed(heal);
    }
    void Die()
    {
        Debug.Log(string.Format("{0}はたおれた", status.charaName));
    }

    //================================================================<以下誘発処理>================================================================

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

    /// <summary被ダメージ時誘発</summary>
    public void OnDamaged(int DMG, bool byOpponent)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnDamaged(DMG, byOpponent); }
    }

    /// <summary被回復時誘発</summary>
    public void OnHealed(int healedValue)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnHealed(healedValue); }
    }

    public CharacterStatus GetCharacterStatus() { return status; }
}
