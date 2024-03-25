using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetContentElement : MonoBehaviour
{
    [SerializeField] private GameObject BackgroundImage;
    [SerializeField] private Text StageTitleText;
    [SerializeField] private Text Difficulty_Text;
    [SerializeField] private Text DungeonInfo;
    [SerializeField] private GameObject ClearedText;

    public int dungeonNum;

    public void setElement(Sprite image, string text_stageTitle, int text_difficulty, string text_dungeonInfo,bool cleared)
    {
        BackgroundImage.GetComponent<Image>().sprite = image;
        BackgroundImage.GetComponent<SelectDeungeonImageController>().SetDungeonNum(dungeonNum);
        StageTitleText.text = text_stageTitle;
        Difficulty_Text.text += text_difficulty.ToString();
        DungeonInfo.text += text_dungeonInfo;
        ClearedText.SetActive(cleared);
    }
}
