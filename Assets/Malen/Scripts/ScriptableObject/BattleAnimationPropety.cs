using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleAnimationPropety", menuName = "ScriptableObjects/BattleAnimationPropety")]
public class BattleAnimationPropety : ScriptableObject
{
    [Header("=====<�U����>(�O�i)=====")]
    [Header("�A�j���[�V�������邩")] public bool isAnimationPlayedOnAttack;
    [Header("�O�i����")] public float forwardDistanceOnAttack;
    [Header("�p�x")] public float angleOnAttack;
    [Header("�A�j���[�V��������")] public float durationOnAttack;

    [Header("\n=====<��_���[�W��>(�U��)=====")]
    [Header("�A�j���[�V�������邩")] public bool isAnimationPlayedOnDamaged;
    [Header("�U���̋���")] public float vibrationStrengthOnDamaged;
    [Header("�U����")] public int vibrationCountOnDamaged;
    [Header("�A�j���[�V��������")] public float durationOnDamaged;

    [Header("\n=====<�񕜎�>(�W�����v)=====")]
    [Header("�A�j���[�V�������邩")] public bool isAnimationPlayedOnHealed;
    [Header("�W�����v����")] public float jumpDistanceOnHealed;
    [Header("�A�j���[�V��������")] public float durationOnHealed;

    [Header("\n=====<�_���[�W�\���p�v���p�e�B>=====")]
    [Header("�\���p�Q�[���I�u�W�F�N�g")] public GameObject indicator;
    [Header("�_���[�W�J���[")] public Color damageColor;
    [Header("�񕜃J���[")] public Color healColor;
    [Header("�ړ��I�t�Z�b�g")] public Vector3 offset;
    [Header("���ł܂ł̎���")] public float timeToDismiss;
    [Header("�\���ʒu�̂΂��")] public float scattering;
} 

