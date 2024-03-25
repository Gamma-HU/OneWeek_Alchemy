using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StEIcon : MonoBehaviour
{
    public Text stackText;

    public void SetStack(int stack) { stackText.text = stack.ToString(); }
}
