using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///�@�X�R�A�I�u�W�F�N�g�̎����̊��N���X
/// </summary>
public abstract class BaseScoreObject : MonoBehaviour
{
    public int score { get; private set; } = 0;
    
    public void SetScore(int _value) { score = _value; }
}
