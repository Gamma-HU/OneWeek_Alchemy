using UnityEngine;
using UnityEngine.UI;

public class FontSizeFitter : MonoBehaviour
{
    const float CONTENT_RATE = 0.95f;

    [SerializeField] bool updateOnAwake = true, everyUpdate = true;
    [SerializeField] Text text;
    int startFontSize;
    Rect rect;

    private void Start()
    {
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        startFontSize = text.fontSize;
        rect = text.rectTransform.rect;

        if (updateOnAwake) UpdateFontSize(rect, text, startFontSize);
    }

    private void Update()
    {
        if (everyUpdate) UpdateFontSize(rect, text, startFontSize);
    }

    private void OnValidate()
    {
        if (text == null) TryGetComponent<Text>(out text);
    }

    /// <summary>
    /// フォントサイズを更新
    /// </summary>
    /// <param name="text">Textを表示するRect。nullでも可</param>
    /// <param name="text">Text</param>
    /// <param name="baseSize">文字が表示される最大のフォントサイズより少し小さい値を指定してください。精度に影響します</param>
    public static void UpdateFontSize(Rect rect, Text text, int baseSize = 300)
    {
        text.fontSize = baseSize;
        float rateX = text.preferredWidth / rect.width;
        float rateY = text.preferredHeight / rect.height;

        if (rateX > rateY)
        {
            text.fontSize = (int)(baseSize / rateX * CONTENT_RATE);
        }
        else
        {
            text.fontSize = (int)(baseSize / rateY * CONTENT_RATE);
        }
    }
    public static void UpdateFontSize(Text text, int baseSize = 300) => UpdateFontSize(text.rectTransform.rect, text, baseSize);
}