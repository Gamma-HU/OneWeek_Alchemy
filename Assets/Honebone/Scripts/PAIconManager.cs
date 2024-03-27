using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PAIconManager : MonoBehaviour
{
    [SerializeField]
    GameObject PAIcon;
   
    public void SetIcon(Sprite sprite)
    {
        var i = Instantiate(PAIcon, transform);
        i.GetComponent<Image>().sprite = sprite;
        Destroy(i, 1f);
    }
}
