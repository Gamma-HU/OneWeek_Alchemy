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

    [Header("����UI�\���ʒu�̗D�揇��")] public Corner[] cornerPriority;
    [Header("�\���ʒu�̃I�t�Z�b�g")] public Vector3 offset;
    [Header("�X�N���[������")] public float scrollDuration;
    [Header("�X�N���[���ҋ@����")] public float scrollWaitTime;
    public Vector3[] defaultPosition;
}