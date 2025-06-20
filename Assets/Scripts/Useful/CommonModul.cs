using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// •Ö—˜ŠÖ”ƒNƒ‰ƒX
/// </summary>
public static class CommonModul
{
    public static bool InRange(int _value,int _min,int _max) {
        return _min <= _value && _value < _max;
    }
}
