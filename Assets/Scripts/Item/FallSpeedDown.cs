using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����Ă���X�s�[�h�𔼕��ɂ���
/// </summary>
public class FallSpeedDown : BaseItem
{
    //�X�s�[�h���擾���邽�߂̃f�[�^
    [SerializeField]
    private BaseScoreData scoreData;
    //�ݒ肷�邽�߂̃X�s�[�h
    private float setSpeed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        setSpeed = scoreData.fallSpeed * 0.5f;
    }
    private new void Update() {
        base.Update();
    }

    /// <summary>
    /// �������^�������
    /// </summary>
    public override void AddEffect() {
        Fruit.SetFallSpeed(setSpeed);
        Fruit.IsHalf = true;
    }
    /// <summary>
    /// ���ʂ�����
    /// </summary>
    public override void DeleteEffect() {
        Fruit.IsHalf = false;
    }

    
}
