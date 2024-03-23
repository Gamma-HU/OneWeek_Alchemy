using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class MaterialMenuContentGenerator : MonoBehaviour
{
    public int item_total_num = 10;
    public Sprite item_image;
    [SerializeField] private GameObject RecipePfb_for_mat;
    [SerializeField] private GameObject framePfb_for_mat;
    [SerializeField] private GameObject namePlatePfb;

    public List<GameObject> unlockedMaterial = new List<GameObject>();//âï˙Ç≥ÇÍÇƒÇ¢ÇÈëfçﬁ

    // Start is called before the first frame update
    void Start()
    {
        GenerateContent(item_total_num);
    }

    private void GenerateContent(int item_total_num)
    {
        GameObject recipePfb = null;
        for (int i = 0; i < item_total_num; i++)
        {
            if(i % 4 == 0)
            {
                recipePfb = Instantiate(RecipePfb_for_mat, transform.position, Quaternion.identity);
                recipePfb.transform.SetParent(this.transform, false);
            }

            GameObject frame = Instantiate(framePfb_for_mat, transform.position, Quaternion.identity);
            frame.transform.SetParent(recipePfb.transform, false);

            GameObject image = frame.transform.GetChild(0).gameObject;
            image.GetComponent<Image>().sprite = item_image;
            image.AddComponent<IconInRecipe>();
            image.GetComponent<IconInRecipe>().namePlatePfb = namePlatePfb;
            image.GetComponent<IconInRecipe>().itemName = unlockedMaterial[i].GetComponent<Item>().GetItemName();
        }
    }
}
