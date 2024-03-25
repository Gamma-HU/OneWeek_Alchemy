using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LayoutGroupManager : MonoBehaviour
{
    public LayoutGroup layoutGroup;
    public float animationDuration = 0.5f;
    public Vector2 moveAmount = new Vector2(100f, 100f); // X軸およびY軸の移動量

    private void Start()
    {
        // このスクリプトをアタッチしたゲームオブジェクトの子要素の数を監視し、変更があった場合に再配置する
        CheckLayoutGroupChanges();
    }

    private void CheckLayoutGroupChanges()
    {
        // 子要素の数を監視し、変更があった場合に再配置する
        int previousChildCount = layoutGroup.transform.childCount;
        DOTween.Sequence().AppendCallback(() =>
        {
            int currentChildCount = layoutGroup.transform.childCount;
            if (previousChildCount != currentChildCount)
            {
                // LayoutGroup の再配置をアニメーションで行う
                layoutGroup.DOKill(); // アニメーション中の場合はキャンセル
                layoutGroup.enabled = false; // レイアウトの自動調整を一時的に無効化
                layoutGroup.enabled = true; // レイアウトの再計算をトリガー
                Vector3 moveVector = new Vector3(moveAmount.x, moveAmount.y, 0f);
                layoutGroup.transform.DOLocalMove(layoutGroup.transform.localPosition + moveVector, animationDuration).SetEase(Ease.OutCubic); // 移動するアニメーションを実行
            }
            previousChildCount = currentChildCount;
        }).SetUpdate(true); // Sequence の更新を毎フレーム行うように設定
    }
}
