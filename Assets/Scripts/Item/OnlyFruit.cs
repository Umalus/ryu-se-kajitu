using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �t���[�c�̂ݍ~�点��A�C�e��
/// </summary>
public class OnlyFruit : BaseItem
{
    /// <summary>
    /// �������^������e��
    /// </summary>
    public override void AddEffect() {
        FruitManager.instance.OnlyFruit = true;
    }
}
