using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フルーツのみ降らせるアイテム
/// </summary>
public class OnlyFruit : BaseItem
{
    /// <summary>
    /// 取ったら与えられる影響
    /// </summary>
    public override void AddEffect() {
        FruitManager.instance.OnlyFruit = true;
    }
}
