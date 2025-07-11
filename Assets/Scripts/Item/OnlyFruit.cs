using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;
using static ItemUtility;

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
    public override async UniTask DeleteEffect() {
        timer = 0.0f;
        while (true) {
            if (timer >= 10.0f)
                break;

            timer += Time.deltaTime;

            await UniTask.DelayFrame(1);

        }
        FruitManager.instance.OnlyFruit = false;
    }
}
