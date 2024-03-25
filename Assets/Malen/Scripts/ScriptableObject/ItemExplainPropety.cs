using UnityEngine;

[CreateAssetMenu(fileName = "ItemExplainPropety", menuName = "ScriptableObjects/ItemExplainPropety")]
public class ItemExplainPropety : ScriptableObject
{
    public enum Corner
    {
        BottomLeft,
        TopLeft,
        TopRight,
        BottomRight
    }

    [Header("説明UI表示位置の優先順位")] public Corner[] cornerPriority;
    [Header("表示位置のオフセット")] public Vector3 offset;
    [Header("スクロール時間")] public float scrollDuration;
    [Header("スクロール待機時間")] public float scrollWaitTime;
    public Vector3[] defaultPosition;
}