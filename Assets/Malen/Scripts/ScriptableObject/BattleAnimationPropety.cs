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
    [Header("�\���p�Q�[���I�u�W�F�N�g")] public GameObject damageIndicator;
    [Header("�_���[�W�J���[")] public Color damageColor;
    [Header("�_���[�W�J���[(�A�E�g���C��)")] public Color damageColorOutline;
    [Header("�񕜃J���[")] public Color healColor;
    [Header("�񕜃J���[(�A�E�g���C��)")] public Color healColorOutline;
    [Header("�ړ��I�t�Z�b�g")] public Vector3 damageOffset;
    [Header("���ł܂ł̎���")] public float damageTimeToDismiss;
    [Header("�\���ʒu�̂΂��")] public float damageScattering;

    [Header("\n=====<��Ԉُ�p�v���p�e�B>=====")]
    [Header("�\���p�Q�[���I�u�W�F�N�g")] public GameObject paramsIndicator;
    [Header("�J���[(���u)")] public Color paramsColor;
    [Header("�f�o�t�J���[")] public Color paramsColorDebuff;
    [Header("�f�o�t�J���[(�A�E�g���C��)")] public Color paramsColorDebuffOutline;
    [Header("�o�t�J���[")] public Color paramsColorBuff;
    [Header("�o�t�J���[(�A�E�g���C��)")] public Color paramsColorBuffOutline;
    [Header("�ړ��I�t�Z�b�g")] public Vector3 paramsOffset;
    [Header("���ł܂ł̎���")] public float paramsTimeToDismiss;
    [Header("�\���ʒu�̂΂��")] public float paramsScattering;
} 

