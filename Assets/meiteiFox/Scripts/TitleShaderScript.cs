using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleShaderScript : MonoBehaviour
{
    ButtonScript buttonScript;
    Material material;
    float hamon_ugokasu = 0;
    private void Awake()
    {
        buttonScript = GetComponent<ButtonScript>();
        material = GetComponent<Image>().material;
    }
    private void Update()
    {
        if (buttonScript.IsClicked)
        {
            hamon_ugokasu += Time.deltaTime * 1.5f;
            material.SetFloat("_Hamon_ugokasu", hamon_ugokasu);
            material.SetFloat("_Hamon_seisei", 1.5f);
        }
    }
}
