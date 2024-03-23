using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{

    [CreateAssetMenu(fileName = "HPBarPropety", menuName = "ScriptableObjects/HPBarPropety")]
    public class HPBarPropety : ScriptableObject
    {
        [Header("HPカラー(多)")] public Color normalColor;
        [Header("HPカラー(中)")] public Color damagedColor;
        [Header("HPカラー(少)")] public Color dangerColor;
        [Header("HPゲージ変動時間")] public float gaugeTransitionTime;
        [Header("HPカラー基準値")] public float[] HPColorCriteria;
    }

    [SerializeField] Image hpBarImage;
    [SerializeField] HPBarPropety hpBarPropety;

    private float displayedHPRatio;

    public void UpdateHPGauge(float targetHPRatio)
    {
        displayedHPRatio = hpBarImage.fillAmount;
        float[] criteria = hpBarPropety.HPColorCriteria;
        DOTween.To(() => displayedHPRatio, x => displayedHPRatio = x, targetHPRatio, hpBarPropety.gaugeTransitionTime)
            .OnUpdate(() =>
            {
                hpBarImage.fillAmount = displayedHPRatio;
                
                if (displayedHPRatio < criteria[0])
                {
                    hpBarImage.color = hpBarPropety.dangerColor;
                }
                else if (displayedHPRatio < criteria[1])
                {
                    hpBarImage.color = hpBarPropety.damagedColor;
                }
                else
                {
                    hpBarImage.color = hpBarPropety.normalColor;
                }
            })
            .SetEase(Ease.Linear)
            .SetUpdate(true);
    }
}
