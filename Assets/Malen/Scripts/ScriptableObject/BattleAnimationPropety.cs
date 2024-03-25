using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleAnimationPropety", menuName = "ScriptableObjects/BattleAnimationPropety")]
public class BattleAnimationPropety : ScriptableObject
{
    [Header("=====<出現時>(前進)=====")]
    [Header("アニメーションするか")] public bool isAnimationPlayedOnEmerge;
    [Header("移動距離")] public float forwardDistanceOnEmerge;
    [Header("アニメーション時間")] public float durationOnEmerge;

    [Header("=====<消滅時>(前進)=====")]
    [Header("アニメーションするか")] public bool isAnimationPlayedOnDisappear;
    [Header("移動距離")] public float forwardDistanceOnDisappear;
    [Header("角度")] public float angleOnDisappear;
    [Header("アニメーション時間")] public float durationOnDisappear;

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

    [Header("\n=====<ダメージ表示用プロパティ>=====")]
    [Header("表示用ゲームオブジェクト")] public GameObject damageIndicator;
    [Header("ダメージカラー")] public Color damageColor;
    [Header("ダメージカラー(アウトライン)")] public Color damageColorOutline;
    [Header("ダメージカラー(影)")] public Color damageColorShadow;
    [Header("回復カラー")] public Color healColor;
    [Header("回復カラー(アウトライン)")] public Color healColorOutline;
    [Header("回復カラー(影)")] public Color healColorShadow;
    [Header("攻撃失敗カラー")] public Color missColor;
    [Header("攻撃失敗カラー(アウトライン)")] public Color missColorOutline;
    [Header("攻撃失敗カラー(影)")] public Color missColorShadow;
    [Header("移動オフセット")] public Vector3 damageOffset;
    [Header("消滅までの時間")] public float damageTimeToDismiss;
    [Header("表示位置のばらつき")] public float damageScattering;

    [Header("\n=====<状態異常用プロパティ>=====")]
    [Header("表示用ゲームオブジェクト")] public GameObject paramsIndicator;
    [Header("デバフカラー")] public Color paramsColorDebuff;
    [Header("デバフカラー(アウトライン)")] public Color paramsColorDebuffOutline;
    [Header("デバフカラー(影)")] public Color paramsColorDebuffShadow;
    [Header("バフカラー")] public Color paramsColorBuff;
    [Header("バフカラー(アウトライン)")] public Color paramsColorBuffOutline;
    [Header("バフカラー(影)")] public Color paramsColorBuffShadow;
    [Header("移動オフセット")] public Vector3 paramsOffset;
    [Header("消滅までの時間")] public float paramsTimeToDismiss;
    [Header("表示位置のばらつき")] public float paramsScattering;

    [Header("\n=====<行動不能表示用プロパティ>=====")]
    [Header("表示用ゲームオブジェクト")] public GameObject stunIndicator;
    [Header("行動不能カラー")] public Color stunColor;
    [Header("行動不能カラー(アウトライン)")] public Color stunColorOutline;
    [Header("行動不能カラー(影)")] public Color stunColorShadow;
    [Header("移動オフセット")] public Vector3 stunOffset;
    [Header("消滅までの時間")] public float stunTimeToDismiss;
    [Header("表示位置のばらつき")] public float stunScattering;
} 

