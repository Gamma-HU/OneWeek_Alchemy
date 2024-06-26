using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveMenu : MonoBehaviour
{
    [SerializeField] float openX;
    [SerializeField] float closeX;
    [SerializeField] float openY;
    [SerializeField] float closeY;
    Vector3 closePosition;
    Vector3 openPosition;
    Vector3 closePosition_vertical;
    Vector3 openPosition_vertical;
    bool isOpen;
    [SerializeField]
    private AudioClip SE_MenuOpenClose;

    // Start is called before the first frame update
    void Start()
    {
        closePosition = GetComponent<RectTransform>().anchoredPosition;
        closePosition.x = closeX;
        GetComponent<RectTransform>().anchoredPosition = closePosition;
        openPosition = closePosition;
        openPosition.x = openX;

        closePosition_vertical = GetComponent<RectTransform>().anchoredPosition;
        closePosition_vertical.y = closeY;
        GetComponent<RectTransform>().anchoredPosition = closePosition_vertical;
        openPosition_vertical = closePosition_vertical;
        openPosition_vertical.y = openY;

        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        GetComponent<RectTransform>().DOAnchorPos(openPosition, 0.5f).SetEase(Ease.OutCubic);
        isOpen = true;

        //Sound Effect
        SEManager seManager = FindFirstObjectByType<SEManager>();
        seManager.PlaySE(SE_MenuOpenClose);
    }

    public void CloseMenu()
    {
        GetComponent<RectTransform>().DOAnchorPos(closePosition, 0.5f).SetEase(Ease.OutCubic);
        isOpen = false;

        //Sound Effect
        SEManager seManager = FindFirstObjectByType<SEManager>();
        seManager.PlaySE(SE_MenuOpenClose);
    }

    public void OpenMenu_vertical()
    {
        GetComponent<RectTransform>().DOAnchorPos(openPosition_vertical, 0.5f).SetEase(Ease.OutCubic);
        transform.SetAsLastSibling();  //背面に表示
        isOpen = true;

        //Sound Effect
        SEManager seManager = FindFirstObjectByType<SEManager>();
        seManager.PlaySE(SE_MenuOpenClose);
    }

    public void CloseMenu_vertical()
    {
        GetComponent<RectTransform>().DOAnchorPos(closePosition_vertical, 0.5f).SetEase(Ease.OutCubic);
        transform.SetSiblingIndex(1); //前面に表示
        isOpen = false;

        //Sound Effect
        SEManager seManager = FindFirstObjectByType<SEManager>();
        seManager.PlaySE(SE_MenuOpenClose);
    }

    public void PushMenuButton()
    {
        if (isOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    public void PushMenuButton_vertical()
    {
        if (isOpen)
        {
            CloseMenu_vertical();
        }
        else
        {
            OpenMenu_vertical();
        }
    }
}
