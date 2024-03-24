using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HPBarPropety", menuName = "ScriptableObjects/HPBarPropety")]
public class HPBarPropety : ScriptableObject
{
    [Header("HPカラー(多)")] public Color normalColor;
    [Header("HPカラー(中)")] public Color damagedColor;
    [Header("HPカラー(少)")] public Color dangerColor;
    [Header("HPゲージ変動時間")] public float gaugeTransitionTime;
    [Header("HPカラー基準値")] public float[] HPColorCriteria;
}
