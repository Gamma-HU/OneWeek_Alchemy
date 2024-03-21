using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAbility : MonoBehaviour
{
    [SerializeField] int exHP;
    [SerializeField] int exATK;
    protected Character character;
    protected Character.CharacterStatus characterStatus;
    public void Init(Character chara)
    {
        character = chara;
        characterStatus = character.GetCharacterStatus();

        characterStatus.HP += exHP;
        characterStatus.ATK += exATK;
    }

    public virtual void OnAttack(int DMG, bool missed) { }
    public virtual void OnAttacked(int DMG, bool missed) { }
    public virtual void OnDamaged(int DMG, bool byOpponent) { }
    public virtual void OnHealed(int healedValue) { }
}
