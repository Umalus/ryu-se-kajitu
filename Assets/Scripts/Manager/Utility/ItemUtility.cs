using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUtility : MonoBehaviour
{
    public static void UseObject(int _category) {
        ItemManager.instance.UseObject(_category);
    }

    public static void UnuseObject(BaseItem _base,int _category) {
        ItemManager.instance.UnuseObject(_base, _category);
    }
}
