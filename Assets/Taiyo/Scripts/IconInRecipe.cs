using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconInRecipe : MonoBehaviour
{
    private string itemName;
    GameObject namePlate;

    private void Start()
    {
        itemName = GetComponent<Item>().GetItemName();
    }

    private void OnMouseEnter()
    {
        
    }
}
