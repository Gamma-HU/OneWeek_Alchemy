using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageIndicator : MonoBehaviour
{

    [HideInInspector] public float timeToDismiss;
    [HideInInspector] public Vector3 offset;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + offset, timeToDismiss));
        sequence.Join(text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 0f), timeToDismiss).SetEase(Ease.InCirc)).OnComplete(() => Destroy(gameObject));
    }
}
