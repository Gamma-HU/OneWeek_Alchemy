using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ExplainText : MonoBehaviour
{
    public Text itemNameText;
    public Text explainText;
    public ScrollRect scrollRect;
    public RectTransform content;
    public ItemExplainPropety itemExplainPropety;

    private void Start()
    {
        scrollRect.content.anchoredPosition = Vector2.one;
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(itemExplainPropety.scrollWaitTime)
                .Append(scrollRect.DOVerticalNormalizedPos(0, itemExplainPropety.scrollDuration).SetEase(Ease.Linear))
                .AppendInterval(itemExplainPropety.scrollWaitTime)
                .AppendCallback(() =>
                {
                    scrollRect.content.anchoredPosition = Vector2.one;
                })
                .SetLoops(-1);
    }
}

