using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> bgms;
    [SerializeField]
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = bgms.Choice();
        audioSource.Play();
        
    }

}
