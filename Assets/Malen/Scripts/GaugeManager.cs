using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{

    [CreateAssetMenu(fileName = "HPBarPropety", menuName = "ScriptableObjects/HPBarPropety")]
    public class HPBarPropety : ScriptableObject
    {
        [Header("HP�J���[(��)")] public Color normalColor;
        [Header("HP�J���[(��)")] public Color damagedColor;
        [Header("HP�J���[(��)")] public Color dangerColor;
        [Header("HP�J���[��l")] public float[] HPColorCriteria;
    }

    [SerializeField] Image image;
    [SerializeField] HPBarPropety hpBarPropety;

    public void UpdateHPGauge(float hpRatio)
    {
        float[] criteria = hpBarPropety.HPColorCriteria;
        if (hpRatio < criteria[0])
        {
            image.color = hpBarPropety.dangerColor;
        }
        else if (hpRatio < criteria[1])
        {
            image.color = hpBarPropety.damagedColor;
        }
        else
        {
            image.color = hpBarPropety.normalColor;
        }
        image.fillAmount = hpRatio;
    }

    private class PA : PassiveAbility
    {
        public override void OnAttacked(int DMG, bool missed)
        {
            Debug.Log(DMG);
        }
    }
}
