using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// スコアのデータ
/// </summary>
[CreateAssetMenu]
public class BaseScoreData : ScriptableObject
{
    [SerializeField]
    public int score;
    [SerializeField]
    public float fallSpeed;
}
