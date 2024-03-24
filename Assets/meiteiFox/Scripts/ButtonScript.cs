using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField,Tooltip("多重クリック防止のための判定")]
    public bool IsClicked = false;
    [SerializeField]
    PanelAnimScript PanelAnimScript;
    Material material;
    AudioSource audioSource;
    private void Awake()
    {
        material = GetComponent<Image>().material;
        audioSource = GetComponent<AudioSource>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsClicked)
        {
            IsClicked = true;
            audioSource.Play();
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
        material.SetFloat("_Hamon_ugokasu", 0);
        material.SetFloat("_Hamon_seisei", 0);
        IsClicked = false;
        SceneManager.LoadScene("Alchemy");
    }
}
