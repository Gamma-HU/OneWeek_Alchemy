using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneTransition))]
public class FieldTestEditor : Editor
{
    // Bar_Slide, Bar_Flip
    private SerializedProperty sceneTransitionObjectsProperty;
    private SerializedProperty sceneTransitionStartIntervalProperty;
    private SerializedProperty sceneTransitionSpeedProperty;
    private SerializedProperty timeUpToSceneTransitionProperty;

    // Sprite
    private SerializedProperty sceneTransitionSpriteProperty;
    private SerializedProperty sceneTransitionSpriteColorProperty;
    private SerializedProperty sceneTransitionMaxScaleProperty;
    private SerializedProperty sceneTransitionSpriteSpeedProperty;

    private void OnEnable()
    {
        // Bar_Slide, Bar_Flip
        sceneTransitionStartIntervalProperty = serializedObject.FindProperty("sceneTransitionStartInterval");
        sceneTransitionSpeedProperty = serializedObject.FindProperty("sceneTransitionSpeed");
        timeUpToSceneTransitionProperty = serializedObject.FindProperty("timeUpToSceneTransition");

        // Sprite
        sceneTransitionSpriteProperty = serializedObject.FindProperty("sceneTransitionSprite");
        sceneTransitionSpriteColorProperty = serializedObject.FindProperty("sceneTransitionSpriteColor");
        sceneTransitionMaxScaleProperty = serializedObject.FindProperty("sceneTransitionMaxScale");
        sceneTransitionSpriteSpeedProperty = serializedObject.FindProperty("sceneTransitionSpriteSpeed");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        GUILayout.Space(10);
        SceneTransition instance = target as SceneTransition;

        if (instance.transitionType == SceneTransition.TransitionType.Bar_Slide || instance.transitionType == SceneTransition.TransitionType.Bar_Flip)
        {
            EditorGUILayout.PropertyField(sceneTransitionStartIntervalProperty, new GUIContent("sceneTransitionStartInterval"));
            EditorGUILayout.PropertyField(sceneTransitionSpeedProperty, new GUIContent("sceneTransitionSpeed"));
            EditorGUILayout.PropertyField(timeUpToSceneTransitionProperty, new GUIContent("timeUpToSceneTransition"));
        }

        if (instance.transitionType == SceneTransition.TransitionType.Sprite)
        {
            EditorGUILayout.PropertyField(sceneTransitionSpriteProperty, new GUIContent("sceneTransitionSprite"));
            EditorGUILayout.PropertyField(sceneTransitionSpriteColorProperty, new GUIContent("sceneTransitionSpriteColor"));
            EditorGUILayout.PropertyField(sceneTransitionMaxScaleProperty, new GUIContent("sceneTransitionMaxScale"));
            EditorGUILayout.PropertyField(sceneTransitionSpriteSpeedProperty, new GUIContent("sceneTransitionSpriteSpeed"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
