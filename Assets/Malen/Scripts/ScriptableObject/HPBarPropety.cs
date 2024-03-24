using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HPBarPropety", menuName = "ScriptableObjects/HPBarPropety")]
public class HPBarPropety : ScriptableObject
{
    [Header("HP�J���[(��)")] public Color normalColor;
    [Header("HP�J���[(��)")] public Color damagedColor;
    [Header("HP�J���[(��)")] public Color dangerColor;
    [Header("HP�Q�[�W�ϓ�����")] public float gaugeTransitionTime;
    [Header("HP�J���[��l")] public float[] HPColorCriteria;
}
