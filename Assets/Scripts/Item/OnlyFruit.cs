using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �t���[�c�̂ݍ~�点��A�C�e��
/// </summary>
public class OnlyFruit : BaseItem
{
    private new void Update() {
        base.Update();
    }

    /// <summary>
    /// �������^������e��
    /// </summary>
    public override void AddEffect() {
        FruitManager.instance.OnlyFruit = true;
    }
    /// <summary>
    /// ���ʂ�����
    /// </summary>
    public override void DeleteEffect() {
        FruitManager.instance.OnlyFruit = false;
    }
}
