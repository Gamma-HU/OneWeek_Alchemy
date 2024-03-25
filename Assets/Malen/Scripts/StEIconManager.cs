using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StEIconManager : MonoBehaviour
{

    [SerializeField] GameObject iconBase;

    public StEIcon SetStEIcon(GameObject StE, int stack)
    {
        //foreach (Transform child in transform) { 
        //    if(child.name == StE.name)
        //    {
        //        return StE.GetComponent<PA_StatusEffects>();
        //    }
        //}
        SpriteRenderer sprite = StE.GetComponent<SpriteRenderer>();
        GameObject icon = Instantiate(iconBase);
        icon.transform.SetParent(transform, false);
        icon.GetComponent<StEIcon>().stackText.text = stack.ToString();
        icon.GetComponent<Image>().sprite = sprite.sprite;
        return icon.GetComponent<StEIcon>();
        //icon.name = StE.name;
        //GameObject stEObj = Instantiate(StE);
        //stEObj.transform.SetParent(icon.transform, false);
        //return StE.GetComponent<PA_StatusEffects>();
    }
}
