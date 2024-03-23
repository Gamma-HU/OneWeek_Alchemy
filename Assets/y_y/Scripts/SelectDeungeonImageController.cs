using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectDeungeonImageController : MonoBehaviour, IPointerClickHandler
{
    private bool isMouswOn = false;
    private UnityEngine.Color imageColor;
    private int dungeonNum;

    [SerializeField] private float value;

    private void Start()
    {
        imageColor = GetComponent<Image>().color;
        print(imageColor);
    }

    private void Update()
    {
        if (isMouswOn)
        {
            GetComponent<Image>().color = new UnityEngine.Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            GetComponent<Image>().color = new UnityEngine.Color(1.0f, 1.0f, 1.0f);
        }
    }

    private void OnMouseEnter()
    {
        isMouswOn = true;
    }

    private void OnMouseExit()
    {
        isMouswOn = false;
    }

    // クリックされたときに呼び出されるメソッド(y_y)
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject pageImage = transform.parent.gameObject;
        GameObject stageContent = pageImage.transform.parent.gameObject;
        GameObject content = stageContent.transform.parent.gameObject;
        GameObject viewPort = content.transform.parent.gameObject;
        GameObject stageSelectView = viewPort.transform.parent.gameObject;

        content.GetComponent<StageSelectViewContentGenerator>().SelectDungeon(dungeonNum);
        stageSelectView.GetComponent<MoveMenu>().CloseMenu_vertical();
        //print("click");
    }

    public void SetDungeonNum(int i)
    {
        dungeonNum = i;
    }
}
