using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����Ă���X�s�[�h�𔼕��ɂ���
/// </summary>
public class FallSpeedDown : BaseItem
{
    [SerializeField]
    private BaseScoreData scoreData;

    private float setSpeed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        setSpeed = scoreData.fallSpeed * 0.5f;
    }
    /// <summary>
    /// �������^����e��
    /// </summary>
    public override void AddEffect() {
        Fruit.SetFallSpeed(setSpeed);
        Fruit.IsHalf = true;
    }

    public override void DeleteEffect() {
        Fruit.IsHalf = false;
    }
}
