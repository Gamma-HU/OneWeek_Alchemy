using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/DungeonData")]
public class DungeonData : ScriptableObject
{
    public string dungeonName;
    public GameObject dungeonEffect;
    public Sprite background;
    [Header("0”Ô–Ú‚©‚ç‡‚ÉÚ“G")]public List<GameObject> enemies;
    public List<GameObject> rewardItems;
}
