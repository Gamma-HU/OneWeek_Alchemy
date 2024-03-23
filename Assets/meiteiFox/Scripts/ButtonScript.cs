using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField,Tooltip("多重クリック防止のための判定")]
    bool IsClicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsClicked)
        {
            IsClicked = true;
            SceneManager.LoadScene("Alchemy");
        } else
        {
            return;
        }
    }
}
