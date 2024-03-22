using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveMenu : MonoBehaviour
{
    [SerializeField] float openX;
    [SerializeField] float closeX;
    Vector3 closePosition;
    Vector3 openPosition;
    bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        closePosition = GetComponent<RectTransform>().anchoredPosition;
        closePosition.x = closeX;
        GetComponent<RectTransform>().anchoredPosition = closePosition;
        openPosition = closePosition;
        openPosition.x = openX;

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
    }

    public void CloseMenu()
    {
        GetComponent<RectTransform>().DOAnchorPos(closePosition, 0.5f).SetEase(Ease.OutCubic);
        isOpen = false;
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
}
