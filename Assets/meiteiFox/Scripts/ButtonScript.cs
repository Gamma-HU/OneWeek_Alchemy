using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField,Tooltip("多重クリック防止のための判定")]
    public bool IsClicked = false;
    [SerializeField]
    PanelAnimScript PanelAnimScript;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsClicked)
        {
            IsClicked = true;
            StartCoroutine(WaitCorou());
            PanelAnimScript.AnimStart();
        } else
        {
            return;
        }
    }
    IEnumerator WaitCorou()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Alchemy");

    }
}
