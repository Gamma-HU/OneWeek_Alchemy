using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    [Header("シーン遷移オブジェクト")] public SceneTransitionObject[] sceneTransitionObjects;
    [Header("アニメーションフェーズ")] public TransitionPhase transitionPhase = TransitionPhase.In;
    [Header("アニメーションタイプ")] public TransitionType transitionType = TransitionType.Bar_Slide;

    // Bar_Slide. Bar_Flip
    [HideInInspector] [Header("帯のアニメーション間隔")] public float sceneTransitionStartInterval;
    [HideInInspector] [Header("帯のアニメーション時間")] public float sceneTransitionSpeed;

    // Tile

    // Sprite
    [HideInInspector] [Header("遷移用スプライト")] public Sprite sceneTransitionSprite;
    [HideInInspector] [Header("スプライトカラー")] public Color sceneTransitionSpriteColor;
    [HideInInspector] [Header("スプライトの最大スケール")] public Vector3 sceneTransitionMaxScale;
    [HideInInspector] [Header("スプライトの拡大時間")] public float sceneTransitionSpriteSpeed;

    [Header("シーン遷移までの時間")] public float timeUpToSceneTransition;

    private string transitionSceneName;
    private bool sceneTransitionFlag = false;
    private GameObject sceneTransitionImages;
    private float sceneTransitionTime = 0;
    private int lastStartedTransitionObjectIndex = -1;

    public enum TransitionPhase { In, Out }
    public enum TransitionType { 
        Bar_Slide,
        Bar_Flip,    
        Tile1,
        Tile2,
        Tile3,
        Sprite 
    }

    [Serializable]
    public class SceneTransitionObject
    {
        public GameObject transitionObject;
        public Vector3 targetPoint;
    }

    private void Start()
    {
        if (transitionPhase == TransitionPhase.Out)
        {
            sceneTransitionFlag = true;
        }

        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Scene Transition Images")
            {
                sceneTransitionImages = child.gameObject;
            }
        }
        sceneTransitionImages.SetActive(true);
        sceneTransitionTime = 0;
    }

    public void FixedUpdate()
    {
        if (sceneTransitionFlag)
        {
            if (transitionType == TransitionType.Bar_Slide)
            {
                sceneTransitionTime += Time.deltaTime;
                if (lastStartedTransitionObjectIndex < sceneTransitionObjects.Length - 1)
                {
                    if (sceneTransitionTime > (lastStartedTransitionObjectIndex + 1) * sceneTransitionStartInterval)
                    {
                        sceneTransitionObjects[lastStartedTransitionObjectIndex + 1].transitionObject.transform.DOLocalMove(sceneTransitionObjects[lastStartedTransitionObjectIndex + 1].targetPoint, sceneTransitionSpeed);
                        lastStartedTransitionObjectIndex++;
                    }
                }
                if (timeUpToSceneTransition < sceneTransitionTime)
                {
                    if (transitionPhase == TransitionPhase.In)
                    {
                        SceneManager.LoadScene(transitionSceneName);
                    }
                    else if (transitionPhase == TransitionPhase.Out)
                    {
                        sceneTransitionImages.SetActive(false);
                        sceneTransitionFlag = false;
                    }
                }
            }

            if (transitionType == TransitionType.Bar_Flip)
            {
                sceneTransitionTime += Time.deltaTime;
                if (lastStartedTransitionObjectIndex < sceneTransitionObjects.Length - 1)
                {
                    if (sceneTransitionTime > (lastStartedTransitionObjectIndex + 1) * sceneTransitionStartInterval)
                    {
                        sceneTransitionObjects[lastStartedTransitionObjectIndex + 1].transitionObject.transform.DOLocalRotate(sceneTransitionObjects[lastStartedTransitionObjectIndex + 1].targetPoint, sceneTransitionSpeed);
                        lastStartedTransitionObjectIndex++;
                    }
                }
                if (timeUpToSceneTransition < sceneTransitionTime)
                {
                    if (transitionPhase == TransitionPhase.In)
                    {
                        SceneManager.LoadScene(transitionSceneName);
                    }
                    else if (transitionPhase == TransitionPhase.Out)
                    {
                        sceneTransitionImages.SetActive(false);
                        sceneTransitionFlag = false;
                    }
                }
            }

            if (transitionType == TransitionType.Sprite)
            {
                if(sceneTransitionTime == 0)
                {
                    sceneTransitionObjects[0].transitionObject.transform.DOScale(sceneTransitionMaxScale, sceneTransitionSpriteSpeed);
                    foreach (Transform child in transform)
                    {
                        if (child.gameObject.name == "Square")
                        {
                            child.gameObject.GetComponent<SpriteRenderer>().color = sceneTransitionSpriteColor;
                        }
                    }

                }
                sceneTransitionTime += Time.deltaTime;

                if (timeUpToSceneTransition < sceneTransitionTime)
                {
                    if (transitionPhase == TransitionPhase.In)
                    {
                        SceneManager.LoadScene(transitionSceneName);
                    }
                    else if (transitionPhase == TransitionPhase.Out)
                    {
                        sceneTransitionImages.SetActive(false);
                        sceneTransitionFlag = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// アニメーションを再生したのち指定したシーンに遷移
    /// </summary>
    public void StartSceneTransition(string sceneName)
    {
        transitionSceneName = sceneName;
        sceneTransitionFlag = true;
    }
}
