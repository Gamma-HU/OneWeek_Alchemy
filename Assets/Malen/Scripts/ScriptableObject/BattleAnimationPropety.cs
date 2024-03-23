using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleAnimationPropety", menuName = "ScriptableObjects/BattleAnimationPropety")]
public class BattleAnimationPropety : ScriptableObject
{
    [Header("=====<攻撃時>(前進)=====")]
    [Header("アニメーションするか")] public bool isAnimationPlayedOnAttack;
    [Header("前進距離")] public float forwardDistanceOnAttack;
    [Header("角度")] public float angleOnAttack;
    [Header("アニメーション時間")] public float durationOnAttack;

    [Header("\n=====<被ダメージ時>(振動)=====")]
    [Header("アニメーションするか")] public bool isAnimationPlayedOnDamaged;
    [Header("振動の強さ")] public float vibrationStrengthOnDamaged;
    [Header("振動回数")] public int vibrationCountOnDamaged;
    [Header("アニメーション時間")] public float durationOnDamaged;

    [Header("\n=====<回復時>(ジャンプ)=====")]
    [Header("アニメーションするか")] public bool isAnimationPlayedOnHealed;
    [Header("ジャンプ距離")] public float jumpDistanceOnHealed;
    [Header("アニメーション時間")] public float durationOnHealed;
} 

