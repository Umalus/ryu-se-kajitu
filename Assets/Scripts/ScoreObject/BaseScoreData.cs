using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaseScoreData : ScriptableObject
{
    [SerializeField]
    public int score;
    [SerializeField]
    public float fallSpeed;
}
