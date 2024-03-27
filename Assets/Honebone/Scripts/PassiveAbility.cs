using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAbility : MonoBehaviour
{
    [SerializeField] string PAName;
    [SerializeField, TextArea(3, 10)] string PAInfo;
    [SerializeField] int exHP;
    [SerializeField] int exATK;
    [SerializeField] Sprite icon;

    protected Character character;
    protected Character.CharacterStatus characterStatus;
    protected BattleManager battleManager;
    public void Init(Character chara,BattleManager bm)
    {
        character = chara;
        characterStatus = character.GetCharacterStatus();
        battleManager = bm;

        characterStatus.maxHP += exHP;
        characterStatus.HP += exHP;
        characterStatus.ATK += exATK;
    }

    public virtual void OnBattleStart() { }
    public virtual void OnStun() { }
    public virtual void OnAttack(int DMG, bool missed) { }
    public virtual void OnAttacked(int DMG, bool missed) { }
    public virtual void OnDamaged(int DMG, bool byOpponent) { }
    public virtual void OnHealed(int healedValue) { }
    public virtual void OnAppliedStE(BattleManager.StEParams applied) { }
    public virtual void OnDie() { }

    public string GetPAName() { return PAName; }
    public string GetInfo()
    {
        string s = "";
        if (exHP > 0) { s += string.Format("�̗�+{0}\n", exHP); }
        if (exATK > 0) { s += string.Format("�U����+{0}\n", exATK); }
        if (PAInfo != "") { s += PAInfo+"\n"; }
        return s;
    }
    public Sprite GetPAIcon() { return icon; }
}
