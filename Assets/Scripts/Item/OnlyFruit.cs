using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameEnum;

/// <summary>
/// フルーツのみ降らせるアイテム
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
    /// 取ったら与えられる影響
    /// </summary>
    public override void AddEffect() {
        FruitManager.instance.OnlyFruit = true;
    }
    /// <summary>
    /// 効果を消す
    /// </summary>
    public override void DeleteEffect() {
        FruitManager.instance.OnlyFruit = false;
    }
}
