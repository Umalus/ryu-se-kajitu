using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フルーツのみ降らせるアイテム
/// </summary>
public class OnlyFruit : BaseItem
{
    private new void Update() {
        base.Update();
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
