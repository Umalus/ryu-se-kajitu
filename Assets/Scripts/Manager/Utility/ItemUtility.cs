using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUtility : MonoBehaviour
{
    public static void UseObject(int _category) {
        ItemManager.instance.UseObject(_category);
    }

    public static async UniTask UnuseObject(BaseItem _base,int _category) {
        await ItemManager.instance.UnuseObject(_base, _category);
    }
}
