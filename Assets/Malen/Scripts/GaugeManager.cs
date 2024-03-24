using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{

    [SerializeField] Image hpBarImage;
    [SerializeField] HPBarPropety hpBarPropety;

    public void UpdateHPGauge(float targetHPRatio)
    {
        float displayedHPRatio = hpBarImage.fillAmount;
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
