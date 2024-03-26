using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;
using static Item;

/// <summary>
/// unlockedMaterialに解放済みの素材を渡せば、動的に素材リストが生成されます
/// </summary>

public class MaterialMenuContentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject RecipePfb_for_mat;
    [SerializeField] private GameObject framePfb_for_mat;
    [SerializeField] private GameObject namePlatePfb;

    [Header("このリストに解放済みの素材を渡してください.\n現状は手動で要素を指定している")]
    public List<GameObject> unlockedMaterial = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        unlockedMaterial = FindObjectOfType<GameManager>().GetUnlockedMaterial();
        GenerateContent(unlockedMaterial.Count);
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
            //image.GetComponent<Image>().sprite = item_image;
            image.AddComponent<IconInRecip_for_mat>();
            image.GetComponent<IconInRecip_for_mat>().namePlatePfb = namePlatePfb;
            image.GetComponent<IconInRecip_for_mat>().itemName = unlockedMaterial[i].GetComponent<Item>().GetItemName();
            image.GetComponent<IconInRecip_for_mat>().Init_AsMaterialIcon(unlockedMaterial[i]);

            ItemData itemData = unlockedMaterial[i].GetComponent<Item>().GetItemData();
            image.GetComponent<Image>().sprite = itemData.itemSprite;
        }
    }
}
