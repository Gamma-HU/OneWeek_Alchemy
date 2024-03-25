using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackout : MonoBehaviour
{
    [SerializeField] Image image;

    public void StartBlackout() { StartCoroutine(cBlackout()); }
    private void Start()
    {
        StartCoroutine(SceneStart());
    }
    IEnumerator cBlackout()
    {
        var wait = new WaitForSeconds(0.05f);
        for (int i = 0; i < 20; i++)
        {
            Color c = image.color;
            c.a += 0.05f;
            image.color = c;
            yield return wait;
        }
    }
    IEnumerator SceneStart()
    {
        var wait = new WaitForSeconds(0.05f);
        for (int i = 0; i < 20; i++)
        {
            Color c = image.color;
            c.a -= 0.05f;
            image.color = c;
            yield return wait;
        }
    }
}
