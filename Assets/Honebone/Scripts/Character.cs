using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [System.Serializable]
    public class CharacterStatus
    {
        public string charaName;

        [Header("�ő�HP")] public int maxHP;
        [Header("�U����")] public int ATK;

        [Header("�����i�A�G�L�����̓����A��Ԉُ�\n(PassiveAbility���A�^�b�`����Ă���GameObjrct)")] public List<GameObject> passiveAbilities;


        [Header("\n\n=====<�ȉ��̃X�e�[�^�X�͐퓬���ɕω�>=====\n\n")]
        [Header("�U�����̃_���[�W�ʂɉ��Z�E��Z")]
        public int exDMG_int;
        public float exDMG_mul;

        [Header("�G����̔�_���[�W�ʂɉ��Z�E��Z")]
        public int PROT_int;
        public float PROT_mul;

        [Header("��񕜗ʂɏ�Z")] public float RHeal_mul;

        [Header("����HP")] public int HP;

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
        foreach (GameObject passiveAbility in status.passiveAbilities)//staus�ɂ���p�b�V�u�A�r���e�B����A�X�N���v�g�����𒊏o(�U�������̍ۂ̊ȗ����̂���)
        {
            var p = Instantiate(passiveAbility, transform);
            p.GetComponent<PassiveAbility>().Init(this);
            passiveAbilities.Add(p.GetComponent<PassiveAbility>());
        }
    }


    public void SetOpponent(Character chara) //�퓬����̐ݒ�
    {
        opponent = chara;
        opponetnStatus = opponent.GetCharacterStatus();
    }
    public void Attack()
    {
        if (status.blind == 0)
        {
            float fDMG = status.ATK;//��b�_���[�W
            float exDMG_mul = Mathf.Max(0f, 1f + status.exDMG_mul - opponetnStatus.PROT_mul);//�_���[�W�{���␳ = 1 + [���g�̗^�_���[�W���␳] - [����̔�_���[�W���␳] (���ɂ͂Ȃ�Ȃ�)
            int exDMG_int = status.exDMG_int - opponetnStatus.PROT_int;//�_���[�W�����␳ = [���g�̗^�_���[�W�␳] - [����̔�_���[�W�␳]
            fDMG = Mathf.Max(0f, (fDMG * exDMG_mul) + exDMG_int);//�_���[�W = ([��b�_���[�W] * [�_���[�W�{���␳]) + [�_���[�W�����␳]
            int DMG = Mathf.RoundToInt(fDMG);
            opponent.Damage(DMG,true);//�l�̌ܓ����đ����Damage�֐��ɓn��
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
        Debug.Log(string.Format("{0}��{1}�_���[�W", status.charaName, DMG));
        status.HP-= DMG;
        OnDamaged(DMG, byOpponent);
        if (status.HP <= 0) { Die(); }
    }
    public void Heal(int value)
    {
        float exHeal = Mathf.Max(0f, 1 + status.RHeal_mul);
        int heal = Mathf.RoundToInt(value * exHeal);
        status.HP += Mathf.Min(status.HP + heal, status.maxHP);
        Debug.Log(string.Format("{0}��{1}��", status.charaName, heal));
        OnHealed(heal);
    }
    void Die()
    {
        Debug.Log(string.Format("{0}�͂����ꂽ", status.charaName));
    }

    //================================================================<�ȉ��U������>================================================================

    /// <summary>�U�����A�����������Ɋւ�炸�U��</summary>
    public void OnAttack(int DMG, bool missed)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnAttack(DMG, missed); }
    }
    /// <summary>�U�����ꂽ���A�����������Ɋւ�炸�U��</summary>
    public void OnAttacked(int DMG, bool missed)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnAttacked(DMG, missed); }
    }

    /// <summary��_���[�W���U��</summary>
    public void OnDamaged(int DMG, bool byOpponent)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnDamaged(DMG, byOpponent); }
    }

    /// <summary��񕜎��U��</summary>
    public void OnHealed(int healedValue)
    {
        List<PassiveAbility> PA = new List<PassiveAbility>(passiveAbilities);
        foreach (PassiveAbility passiveAbility in PA) { passiveAbility.OnHealed(healedValue); }
    }

    public CharacterStatus GetCharacterStatus() { return status; }
}
