using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyFruit : BaseItem
{
    public override void AddEffect() {
        FruitManager.instance.OnlyFruit = true;
    }
}
