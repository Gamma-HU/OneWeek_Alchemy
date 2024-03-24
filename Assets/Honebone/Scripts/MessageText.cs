using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageText : MonoBehaviour
{
    [SerializeField] Text message;
    [SerializeField] Image background;
    [SerializeField] Color messageDefaultColor;
    [SerializeField] Color backgroundDefaultColor;


    Coroutine current;
   public void SetMessage(string s,float extend)
    {
        message.color = messageDefaultColor;
        message.text = s;
        background.color = backgroundDefaultColor;
        if(current != null) { StopCoroutine(current); }
        current = StartCoroutine(FadeOut(extend));
    }

   IEnumerator FadeOut(float extend)
    {
        Color c;
        yield return new WaitForSeconds(extend);
        for(int i = 0; i < 9; i++)
        {
            c = background.color;
            c.a -= 0.1f;
            background.color = c;

            c = message.color;
            c.a -= 0.1f;
            message.color = c;
            yield return new WaitForSeconds(0.15f);
        }
        message.text = "";
    }
}
