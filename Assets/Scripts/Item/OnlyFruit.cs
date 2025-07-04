using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;

/// <summary>
/// �t���[�c�̂ݍ~�点��A�C�e��
/// </summary>
public class OnlyFruit : BaseItem
{
    private static bool IsRunningTime = false;
    [SerializeField]
    float timer = 0.0f;
    private void Start() {
        categoryID = (int)eItemCategory.OnlyFruit;
    }

    private new void Update() {
        base.Update();
        if (IsRunningTime)
            timer += Time.deltaTime;

        if(timer >= 10.0f) {
            DeleteEffect();
            Destroy(gameObject);
        }
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
