using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// スコアのデータ
/// </summary>
[CreateAssetMenu]
public class BaseScoreData : ScriptableObject
{
    [Header("基礎スコア")]
    public int score;
    [Header("落下スピード")]
    public float fallSpeed;
    [Header("スコア倍率")]
    public float scoreRatio = 1.0f;

}
