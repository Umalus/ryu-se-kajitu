using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �X�R�A�̃f�[�^
/// </summary>
[CreateAssetMenu]
public class BaseScoreData : ScriptableObject
{
    [SerializeField]
    public int score;
    [SerializeField]
    public float fallSpeed;
}
